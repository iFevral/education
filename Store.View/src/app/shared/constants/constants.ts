import { EnumsAttributes } from 'src/app/shared/constants/enums-attributes';
import { ApiUrls } from 'src/app/shared/constants/api-urls';
import { FormValidatorParams } from 'src/app/shared/constants/form-validator-params';
import { DisplayedColumns } from 'src/app/shared/constants/displayed-columns';
import { StripeConfig } from 'src/app/shared/constants/stripe-config';

export class Constants {
    public static apiUrls: ApiUrls = new ApiUrls();
    public static enumsAttributes: EnumsAttributes = new EnumsAttributes();
    public static formValidatorParams: FormValidatorParams = new FormValidatorParams();
    public static displayedColumns: DisplayedColumns = new DisplayedColumns(); 
    public static stripeConfig: StripeConfig = new StripeConfig();
}
