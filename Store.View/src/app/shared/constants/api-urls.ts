import { environment } from '../../../environments/environment';

export class ApiUrls {
    public apiUrl = environment.apiUrl;
    public refreshTokenUrl = `${this.apiUrl}/RefreshToken`;
 
    public userControllerUrl = `${this.apiUrl}/Users/`;
    public orderControllerUrl = `${this.apiUrl}/Orders/`;
    public authorControllerUrl = `${this.apiUrl}/Authors/`;
    public accountControllerUrl = `${this.apiUrl}/Account/`;
    public printingEditionControllerUrl = `${this.apiUrl}/PrintingEditions/`;

    public authenticationUrl = `${this.accountControllerUrl}SignIn/`;
    public authorizationUrl = `${this.accountControllerUrl}SignUp/`;
    public passwordResetingUrl = `${this.accountControllerUrl}ForgotPassword/`;
    public emailConfirmingUrl = `${this.accountControllerUrl}ConfirmEmail/`;
    public userBlockingUrl = `${this.userControllerUrl}Block/`;
    public userRoleUrl = `${this.accountControllerUrl}GetRole/`;

    public orderByUserUrl = `${this.orderControllerUrl}GetByUser/`;
}
