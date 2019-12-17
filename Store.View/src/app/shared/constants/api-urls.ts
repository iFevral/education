import { environment } from '../../../environments/environment';

export class ApiUrls {
    apiUrl = environment.apiUrl;
    refreshTokenUrl = `${this.apiUrl}/RefreshToken`;

    userControllerUrl = `${this.apiUrl}/Users/`;
    orderControllerUrl = `${this.apiUrl}/Orders/`;
    authorControllerUrl = `${this.apiUrl}/Authors/`;
    accountControllerUrl = `${this.apiUrl}/Account/`;
    printingEditionControllerUrl = `${this.apiUrl}/PrintingEditions/`;

    authenticationUrl = `${this.accountControllerUrl}SignIn/`;
    authorizationUrl = `${this.accountControllerUrl}SignUp/`;
    passwordResetingUrl = `${this.accountControllerUrl}ForgotPassword/`;
    emailConfirmingUrl = `${this.accountControllerUrl}ConfirlEmail/`;

    userBlockingUrl = `${this.userControllerUrl}Block/`;
}
