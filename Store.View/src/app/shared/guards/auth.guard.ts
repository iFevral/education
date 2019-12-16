import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AccountService } from '../services';


@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    private currentUser: any;
    constructor(
        private router: Router,
        private accountService: AccountService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        this.accountService.getTokens().subscribe(data => this.currentUser = data);
        if (this.currentUser) {
            return true;
        }

        this.router.navigate(['Account/SignIn'], { queryParams: { returnUrl: state.url } });
        return false;
    }
}