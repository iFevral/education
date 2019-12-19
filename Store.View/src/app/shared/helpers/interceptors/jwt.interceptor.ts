import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpResponse } from '@angular/common/http';

import { catchError, tap } from 'rxjs/operators';
import { Observable, throwError, from, BehaviorSubject } from 'rxjs';

import { TokenModel } from '../../models';
import { AccountService } from '../../services';
import { Constants } from '../../constants/constants';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    private tokenModel: BehaviorSubject<TokenModel>;

    constructor(private accountService: AccountService) {
        this.tokenModel = this.accountService.getTokens();
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const accessToken = this.tokenModel.value
            ? this.tokenModel.value.accessToken
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

                        const requestWithNewToken = this.getRequestWithToken(request, this.tokenModel.value.accessToken);
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

        const refreshRequest = this.getRequestWithToken(request, this.tokenModel.value.refreshToken, true);

        const refreshEvent = next.handle(refreshRequest)
            .pipe(tap(data => {
                if (data instanceof HttpResponse) {
                    localStorage.setItem('tokens', JSON.stringify(data.body));
                    this.tokenModel.next(data.body);
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
            const flag = localStorage.getItem('isRememberMeActivated');
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