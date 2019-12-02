namespace Store.BusinessLogic.Common
{
    public partial class Constants
    {
        public const string EditUserError = "Editing user has failed";

        public partial class Errors
        {
            public const string EditUserError = "Editing user has failed";
            public const string CreateUserError = "Creating user has failed";
            public const string DeleteUserError = "Deleting user has failed";
            public const string CreateRoleError = "Creating role has failed";
            public const string DeleteRoleError = "Deleting role has failed";
            public const string UserExistsError = "Users has already created.";
            public const string UnlockUserError = "Unlock has failed";
            public const string EmailExistsError = "Email is already registered";
            public const string RoleNotExistsError = "Role doesn`t exists";
            public const string UserNotExistsError = "User not found.";
            public const string UsersNotExistError = "Users not found.";
            public const string UserNotInRoleError = "User is not in role.";
            public const string UserNotInAnyRoleError = "User has not any role.";
            public const string WrongCredentialsError = "Username or password is incorrect. Please check data.";
            public const string EmailConfirmationError = "Email confirmation error";

            public const string CreateAuthorError = "Creating author has failed";
            public const string UpdateAuthorError = "Updating author has failed";
            public const string DeleteAuthorError = "Deleting author has failed";
            public const string NotFoundAuthorError = "Author not found";

            public const string CreatePaymentError = "Creating payment has failed";
            public const string RemovePaymentError = "Removing payment has failed";

            public const string CreateOrderError = "Creating order has failed";
            public const string UpdateOrderError = "Updating order has failed";
            public const string DeleteOrderError = "Deleting order has failed";
            public const string NotFoundOrderError = "Order not found";

            public const string CreatePringtingEditionError = "Creating pringting Edition has failed";
            public const string UpdatePringtingEditionError = "Updating pringting Edition has failed";
            public const string DeletePringtingEditionError = "Deleting pringting Edition has failed";
            public const string NotFoundPringtingEditionError = "Printing edition not found";
        }
    }
}
