import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import { BehaviorSubject, Observable } from 'rxjs';

import { TokenModel, UserModelItem, RegistrationResultModel, BaseModel } from '../models';
import { Constants } from '../constants/constants';
import { RoleName } from '../enums';
import { MatSnackBar } from '@angular/material';

@Injectable({ providedIn: 'root' })
export class AccountService {

    private tokenSubject: BehaviorSubject<TokenModel>;
    private profileSubject: BehaviorSubject<UserModelItem>;
    private rememberMeSubject: BehaviorSubject<boolean>;

    constructor(private http: HttpClient, private router: Router, private messageContainer: MatSnackBar) {

        let tokenModel: TokenModel = JSON.parse(localStorage.getItem('tokens'));
        if (!tokenModel) {
            tokenModel = new TokenModel();
        }
        this.tokenSubject = new BehaviorSubject<TokenModel>(tokenModel);

        let accountModel: UserModelItem = JSON.parse(localStorage.getItem('profile'));
        if (!accountModel) {
            accountModel = new UserModelItem();
        }
        this.profileSubject = new BehaviorSubject<UserModelItem>(accountModel);

        let isRememberMeActivated: boolean = JSON.parse(localStorage.getItem('rememberMeFlag'));
        if (!isRememberMeActivated) {
            isRememberMeActivated = false;
        }
        this.rememberMeSubject = new BehaviorSubject<boolean>(isRememberMeActivated);
    }

    public setTokens(tokenModel) {
        localStorage.setItem('tokens', JSON.stringify(tokenModel));
        this.tokenSubject.next(tokenModel);
    }

    public getTokens(): Observable<TokenModel> {
        return this.tokenSubject.asObservable();
    }

    public setProfile() {
        this.http.post<UserModelItem>(Constants.apiUrls.accountControllerUrl, null)
            .subscribe(
                result => {
                    localStorage.setItem('profile', JSON.stringify(result));
                    this.profileSubject.next(result);
                },
                error => {
                    localStorage.removeItem('profile');
                }
            );
    }

    public getProfile(): Observable<UserModelItem> {
        return this.profileSubject.asObservable();
    }

    public setRememberMeFlag(rememberMeFlag: boolean) {
        localStorage.setItem('rememberMeFlag', JSON.stringify(rememberMeFlag));
        this.rememberMeSubject.next(rememberMeFlag);
    }

    public getRememberMeFlag(): Observable<boolean> {
        return this.rememberMeSubject.asObservable();
    }

    public editProfile(userModel: UserModelItem): void {
        this.http.patch<BaseModel>(Constants.apiUrls.accountControllerUrl, userModel).subscribe((resultModel: BaseModel) => {
            if (resultModel.errors.length > 0) {
                this.showDialogMessage(resultModel.errors.toString());
            }
        });
    }

    public signIn(email: string, password: string, rememberMeFlag: boolean) {

        this.http.post<TokenModel>(Constants.apiUrls.authenticationUrl + rememberMeFlag, { email, password })
            .subscribe(data => {
                if (data && data.accessToken && data.refreshToken) {
                    this.setProfile();
                    this.setTokens(data);
                    this.setRememberMeFlag(rememberMeFlag);
                    this.redirectTo('/');
                } else {
                    this.showDialogMessage(data.errors.toString());
                }
            });
    }

    public signUp(credentials: UserModelItem) {
        this.http.post<RegistrationResultModel>(Constants.apiUrls.authorizationUrl, credentials)
            .subscribe((data: RegistrationResultModel) => {
                if (data && data.message) {
                    this.showDialogMessage(data.message);
                    this.redirectTo('/');
                } else {
                    this.showDialogMessage(data.errors.toString());
                }
            });
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


    private showDialogMessage(message: string) {
        if (message && message !== '') {
            this.messageContainer.open(message, 'X', {
                duration: 5000,
                verticalPosition: 'top'
            });
        }
    }
}
