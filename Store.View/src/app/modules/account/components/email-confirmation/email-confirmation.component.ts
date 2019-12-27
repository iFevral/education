import { Component } from '@angular/core';
import { AccountService } from '../../../../shared/services';
import { BaseModel } from '../../../../shared/models';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-email-confirmation',
    templateUrl: './email-confirmation.component.html',
    styleUrls: ['./email-confirmation.component.scss']
})
export class EmailConfirmationComponent {
    private message: string;

    constructor(
        route: ActivatedRoute,
        private accountService: AccountService
    ) {
        route.queryParams.subscribe(params => {
            this.accountService.confirmEmail(params.email, params.token).subscribe((resultModel: BaseModel) => {
                this.message = resultModel.errors.length > 0
                    ? resultModel.errors.toString()
                    : 'Your email has successfully confirmed.';
            });
        });

    }

}
