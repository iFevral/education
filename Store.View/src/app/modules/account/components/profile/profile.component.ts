import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { Constants } from 'src/app/shared/constants/constants';
import { AccountService } from 'src/app/shared/services';

import { UserModelItem } from 'src/app/shared/models';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

    private isEditMode: boolean;
    private editForm: FormGroup;
    private userModel: UserModelItem;
    private passwordConfirmationFormValue: string;

    constructor(private accountService: AccountService) {

        this.isEditMode = false;
        this.userModel = new UserModelItem();

        this.editForm = new FormGroup({
            firstName: new FormControl('', [
                Validators.minLength(Constants.formValidatorParams.nameMinLength),
                Validators.maxLength(Constants.formValidatorParams.nameMaxLength),
                Validators.required,
            ]),
            lastName: new FormControl('', [
                Validators.minLength(Constants.formValidatorParams.nameMinLength),
                Validators.maxLength(Constants.formValidatorParams.nameMaxLength),
                Validators.required
            ]),
            email: new FormControl('', [
                Validators.minLength(Constants.formValidatorParams.emailMinLength),
                Validators.maxLength(Constants.formValidatorParams.emailMaxLength),
                Validators.required,
                Validators.email
            ]),
            oldPassword: new FormControl('', [
            ]),
            newPassword: new FormControl('', [
                Validators.minLength(Constants.formValidatorParams.passwordMinLength),
                Validators.maxLength(Constants.formValidatorParams.passwordMaxLength),
            ]),
            confirmPassword: new FormControl('', [
            ]),
        });
    }

    public ngOnInit(): void {
        this.accountService.getProfile().subscribe((resultModel: UserModelItem) => {
            this.userModel = resultModel;
        });
    }

    public changeMode(): void {
        this.isEditMode = !this.isEditMode;
    }

    public editProfile(): void {
        this.accountService.editProfile(this.userModel);
    }

    public cancelEditProfile(): void {
        this.userModel.password = '';
        this.userModel.newPassword = '';
        this.passwordConfirmationFormValue = '';
    }

    public setAvatar(event): void {

        if (event.target.files.length === 0) {
            return;
        }

        const newImage = event.target.files[0];
        const fileReader = new FileReader();

        fileReader.onload = (fileLoadedEvent) => {
            this.userModel.avatar = (<FileReader>fileLoadedEvent.target).result.toString();
            this.accountService.editProfile(this.userModel);
        };

        fileReader.readAsDataURL(newImage);

    }
}
