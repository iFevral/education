import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpResponse } from '@angular/common/http';

import { catchError, tap } from 'rxjs/operators';
import { Observable, throwError, from, BehaviorSubject } from 'rxjs';

import { TokenModel } from 'src/app/shared/models';
import { AccountService } from 'src/app/shared/services';
import { Constants } from 'src/app/shared/constants/constants';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    private tokenModel: TokenModel;

    constructor(private accountService: AccountService) {
        this.accountService.getTokens().subscribe((tokenModel: TokenModel) => {
            this.tokenModel = tokenModel;
        });
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const accessToken = this.tokenModel
            ? this.tokenModel.accessToken
            : null;

        request = this.getRequestWithToken(request, accessToken);

        const event = next.handle(request)
            .pipe(catchError(accessError => {
                if (accessError.status !== 401) {
                    this.accountService.redirectTo('/');
                    throw throwError(accessError);
                }

                const refreshRequest = this.refreshToken(request, next)
                    .then(() => {

                        const requestWithNewToken = this.getRequestWithToken(request, this.tokenModel.accessToken);
                        const repeatedRequest = next.handle(requestWithNewToken).toPromise().then(response => {
                            return response;
                        });

                        return repeatedRequest;
                    });
                return refreshRequest;
            }));

        return from(event);
    }

    public refreshToken(request: HttpRequest<any>, next: HttpHandler): Promise<HttpEvent<any>> {

        const refreshRequest = this.getRequestWithToken(request, this.tokenModel.refreshToken, true);

        const refreshEvent = next.handle(refreshRequest)
            .pipe(tap(data => {
                if (data instanceof HttpResponse) {
                    this.accountService.setTokens(data.body);
                }
            }), catchError(err => {
                if (err.status === 401) {
                    this.accountService.signOut();
                }
                throw throwError(err);
            })).toPromise();

        return refreshEvent;
    }

    public getRequestWithToken(request: HttpRequest<any>, token: string, isRefresh: boolean = false) {
        let url: string = request.url;
        let method: string = request.method;

        if (!token) {
            return request;
        }

        if (isRefresh) {
            const flag = this.accountService.getRememberMeFlag();
            const isRememberMeActivated: boolean = flag ? Boolean(flag) : false;
            url = Constants.apiUrls.refreshTokenUrl + isRememberMeActivated;
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