import { EnumsKeys } from 'src/app/shared/constants/enums-keys';
import { EnumsValues } from 'src/app/shared/constants/enums-values';
import { ApiUrls } from 'src/app/shared/constants/api-urls';
import { FormValidatorParams } from 'src/app/shared/constants/form-validator-params';
import { DisplayedColumns } from 'src/app/shared/constants/displayed-columns';
import { StripeConfig } from 'src/app/shared/constants/stripe-config';

export class Constants {
    public static apiUrls: ApiUrls = new ApiUrls();
    public static enumsKeys: EnumsKeys = new EnumsKeys();
    public static enumsValues: EnumsValues = new EnumsValues();
    public static formValidatorParams: FormValidatorParams = new FormValidatorParams();
    public static displayedColumns: DisplayedColumns = new DisplayedColumns(); 
    public static stripeConfig: StripeConfig = new StripeConfig();
}
