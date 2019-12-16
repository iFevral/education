import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';

import { TokenModel, UserModelItem, RegistrationResultModel } from '../models';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AccountService {

    private tokenSubject: BehaviorSubject<TokenModel>;

    constructor(private http: HttpClient, private router: Router) {
        let model: TokenModel = JSON.parse(localStorage.getItem('tokens'));
        if (!model) {
            model = new TokenModel();
        }
        this.tokenSubject = new BehaviorSubject<TokenModel>(model);
    }

    public getTokens(): BehaviorSubject<TokenModel> {
        return this.tokenSubject;
    }

    public signIn(email: string, password: string): Observable<TokenModel> {

        const result = this.http.post<TokenModel>(`${environment.apiUrl}/Account/SignIn`, { email, password })
            .pipe(map(data => {

                if (data && data.accessToken && data.refreshToken) {
                    localStorage.setItem('tokens', JSON.stringify(data));
                    console.log(this.tokenSubject);
                }
                return data;
            }));

        result.subscribe(data => {
            this.tokenSubject.next(data);
        });
        return result;
    }

    public signUp(credentials: UserModelItem): Observable<RegistrationResultModel> {
        const result = this.http.post<RegistrationResultModel>(`${environment.apiUrl}/Account/SignUp`, credentials);
        return result;
    }

    public signOut() {
        const model: TokenModel = new TokenModel();
        localStorage.removeItem('tokens');
        this.tokenSubject.next(model);
        this.router.navigate(['/']);
    }

    public resetPassword(email: string) {
        const result = this.http.post<RegistrationResultModel>(`${environment.apiUrl}/Account/ForgotPassword`, { email });
        return result;
    }

    public log() {
        console.log(localStorage.getItem('tokens'));
        console.log(this.tokenSubject);
    }
}
