import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';

import { TokenModel, UserModelItem, RegistrationResultModel, BaseModel } from '../models';
import { Constants } from '../constants/constants';
import { RoleName } from '../enums';

@Injectable({ providedIn: 'root' })
export class AccountService {

    private tokenSubject: BehaviorSubject<TokenModel>;
    private roleSubject: BehaviorSubject<RoleName>;

    constructor(private http: HttpClient, private router: Router) {

        let tokenModel: TokenModel = JSON.parse(localStorage.getItem('tokens'));
        if (!tokenModel) {
            tokenModel = new TokenModel();
        }
        this.tokenSubject = new BehaviorSubject<TokenModel>(tokenModel);

        let accountModel: RoleName = JSON.parse(localStorage.getItem('role'));
        if (!accountModel) {
            accountModel = RoleName.None;
        }
        this.roleSubject = new BehaviorSubject<RoleName>(accountModel);
    }

    public getTokens(): BehaviorSubject<TokenModel> {
        return this.tokenSubject;
    }

    public setRole(): Observable<RoleName> {
        return this.http.post<RoleName>(Constants.apiUrls.userRoleUrl, null);
    }

    public getRole(): BehaviorSubject<RoleName> {
        return this.roleSubject;
    }

    public getProfile(): Observable<UserModelItem> {
        const response = this.http.post<UserModelItem>(Constants.apiUrls.accountControllerUrl, null);
        return response;
    }

    public editProfile(userModel: UserModelItem): Observable<BaseModel> {
        const response = this.http.patch<BaseModel>(Constants.apiUrls.accountControllerUrl, userModel);
        return response;
    }

    public signIn(email: string, password: string, isRememberMeActivated: boolean): Observable<TokenModel> {

        const response = this.http.post<TokenModel>(Constants.apiUrls.authenticationUrl + isRememberMeActivated, { email, password })
            .pipe(map(data => {

                if (data && data.accessToken && data.refreshToken) {
                    localStorage.setItem('tokens', JSON.stringify(data));
                }
                return data;
            }));

        response.subscribe(data => {

            this.setRole().subscribe((result: RoleName) => {
                localStorage.setItem('role', result.toString());
                this.roleSubject.next(result);
            });

            this.tokenSubject.next(data);
        });
        return response;
    }

    public signUp(credentials: UserModelItem): Observable<RegistrationResultModel> {
        const response = this.http.post<RegistrationResultModel>(Constants.apiUrls.authorizationUrl, credentials);
        return response;
    }

    public signOut() {
        const model: TokenModel = new TokenModel();
        localStorage.clear();
        this.tokenSubject.next(model);
        this.redirectTo('/');
    }

    public resetPassword(email: string) {
        const response = this.http.post<RegistrationResultModel>(Constants.apiUrls.passwordResetingUrl, { email });
        return response;
    }

    public redirectTo(path: string) {
        this.router.navigate([path]);
    }

    public log() {
        console.log(localStorage.getItem('role'));
        console.log(localStorage.getItem('tokens'));
        console.log(this.tokenSubject);
    }
}
