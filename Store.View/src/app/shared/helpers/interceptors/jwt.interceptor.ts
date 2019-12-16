import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpResponse } from '@angular/common/http';

import { catchError, tap } from 'rxjs/operators';
import { Observable, throwError, from, BehaviorSubject } from 'rxjs';

import { TokenModel } from '../../models';
import { AccountService } from '../../services';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    private tokenModel: BehaviorSubject<TokenModel>;
    private isAccessExpired: boolean = false;
    constructor(private accountService: AccountService) {
        this.tokenModel = this.accountService.getTokens();
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (this.tokenModel.value && this.tokenModel.value.accessToken) {
            request = this.getRequestWithToken(request, this.tokenModel.value.accessToken);

            const event = next.handle(request)
                .pipe(catchError(accessError => {

                    if (accessError.status === 401) {
                        const refreshRequest = this.getRequestWithToken(request, this.tokenModel.value.refreshToken, true);

                        const refreshEvent = next.handle(refreshRequest)
                            .pipe(tap(data => {
                                if (data instanceof HttpResponse) {
                                    localStorage.setItem('tokens', JSON.stringify(data.body));
                                    this.tokenModel.next(data.body);
                                }
                            }),
                                catchError(err => {
                                    if (err.status === 401) {
                                        this.accountService.signOut();
                                    }
                                    throw throwError(err);
                                }))
                            .toPromise()
                            .then(() => {
                                const requestWithNewToken = this.getRequestWithToken(request, this.tokenModel.value.accessToken);
                                const retryEvent = next.handle(requestWithNewToken).toPromise().then(data => {
                                    return data;
                                });

                                return retryEvent;
                            })
                        return refreshEvent;
                    }

                    throw throwError(accessError);

                }));


            return from(event);
        }

        return next.handle(request).pipe(catchError(err => {

            if (err.status === 401) {
                this.accountService.signOut();
            }

            throw throwError(err);
        }));

    }

    getRequestWithToken(request: HttpRequest<any>, token: string, isRefresh: boolean = false) {
        let url: string = request.url;
        let method: string = request.method;
        if (!token) {
            return request;
        }

        if (isRefresh) {
            url = 'https://localhost:44312/RefreshToken';
            method = 'POST';
        }

        return request.clone({
            url,
            method,
            setHeaders: {
                Authorization: `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });
    }
}