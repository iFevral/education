import { Component } from '@angular/core';
import { AccountService } from '../../../../shared/services';
import { UserModelItem } from '../../../../shared/models';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {

    private isEditMode: boolean = false;
    private editForm: FormGroup;
    private userModel: UserModelItem = new UserModelItem();
    private passwordConfirmation: string;
    constructor(private accountService: AccountService) {

        this.editForm = new FormGroup({
            firstName: new FormControl('', [
                Validators.minLength(3),
                Validators.maxLength(40),
                Validators.required,
            ]),
            lastName: new FormControl('', [
                Validators.minLength(3),
                Validators.maxLength(40),
                Validators.required
            ]),
            email: new FormControl('', [
                Validators.minLength(5),
                Validators.maxLength(40),
                Validators.required,
                Validators.email
            ]),
            oldPassword: new FormControl('', [
            ]),
            newPassword: new FormControl('', [
                Validators.minLength(6),
                Validators.maxLength(40),
            ]),
            confirmPassword: new FormControl('', [
            ]),
        });


        this.accountService.getProfile().subscribe((resultModel: UserModelItem) => {
            this.userModel = resultModel;
        });
    }

    public changeMode() {
        this.isEditMode = !this.isEditMode;
    }

    public editProfile() {
        this.accountService.editProfile(this.userModel).subscribe(data => {
            console.log(data);
        });
    }

    public cancelEditProfile() {
        this.userModel.password = '';
        this.userModel.newPassword = '';
        this.passwordConfirmation = '';
    }
}
