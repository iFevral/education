import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { TokenModel } from '../models/user/token.model';
import { environment } from '../../../environments/environment';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AccountService {
    private tokenSubject: BehaviorSubject<TokenModel>;

    constructor(private http: HttpClient, private router: Router) {
        this.tokenSubject = new BehaviorSubject<TokenModel>(JSON.parse(localStorage.getItem('tokens')));
        
    }

    public getTokens() {
        return this.tokenSubject;
    }

    public login(email: string, password: string): Observable<boolean> {
        return this.http.post<TokenModel>(`${environment.apiUrl}/Account/SignIn`, { email, password })
            .pipe(map(data => {
                if (data && data.accessToken && data.refreshToken) {
                    localStorage.setItem('tokens', JSON.stringify(data));
                    this.tokenSubject.next(data);
                    return true;
                }
                return false;
            }));
    }

    public logout() {
        localStorage.removeItem('tokens');
        this.tokenSubject.next(null);
        this.router.navigate(['/']);
    }

    public log() {
        console.log(localStorage.getItem('tokens'));
        console.log(this.tokenSubject);
    }
}
