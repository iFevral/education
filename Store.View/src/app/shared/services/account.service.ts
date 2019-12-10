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
        this.tokenSubject = new BehaviorSubject<TokenModel>(JSON.parse(localStorage.getItem('tokens')));

    }

    public getTokens() {
        return this.tokenSubject;
    }

    public signIn(email: string, password: string): Observable<boolean> {

        const result = this.http.post<TokenModel>(`${environment.apiUrl}/Account/SignIn`, { email, password })
            .pipe(map(data => {
                if (data && data.accessToken && data.refreshToken) {

                    localStorage.setItem('tokens', JSON.stringify(data));

                    this.tokenSubject.next(data);

                    return true;
                }
                return false;
            }));

        return result;
    }

    public signUp(credentials: UserModelItem): Observable<RegistrationResultModel> {
        const result = this.http.post<RegistrationResultModel>(`${environment.apiUrl}/Account/SignUp`, credentials);

        return result;
    }

    public signOut() {
        localStorage.removeItem('tokens');
        this.tokenSubject.next(null);
        this.router.navigate(['/']);
    }

    public log() {
        console.log(localStorage.getItem('tokens'));
        console.log(this.tokenSubject);
    }
}
