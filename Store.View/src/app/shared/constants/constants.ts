import { EnumsAttributes } from './enums-attributes';
import { ApiUrls } from './api-urls';
import { FormValidatorParams } from './form-validator-params';
import { DisplayedColumns } from './displayed-columns';
import { StripeConfig } from './stripe-config';

export class Constants {
    public static apiUrls: ApiUrls = new ApiUrls();
    public static enumsAttributes: EnumsAttributes = new EnumsAttributes();
    public static formValidatorParams: FormValidatorParams = new FormValidatorParams();
    public static displayedColumns: DisplayedColumns = new DisplayedColumns(); 
    public static stripeConfig: StripeConfig = new StripeConfig();
}
