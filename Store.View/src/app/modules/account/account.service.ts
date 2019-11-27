import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './shared/user.model';

@Injectable()
export class AccountService {
    private url = '/Account';

    constructor(private http: HttpClient) {}

    GetUserById(id: number) {}

    GetUserByName(username: string) {}

    SignIn() {}

    SignUp() {}

    SignOut() {}

}
