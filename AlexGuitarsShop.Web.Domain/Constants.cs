namespace AlexGuitarsShop.Web.Domain;

public static class Constants
{
    public static class Account
    {
        public const string IncorrectAccount = "The information about the account is not filled correctly!";
    }

    public static class Cart
    {
        public const string Key = "Cart";
        public const string CartEmpty = "Cart is empty";
        public const string ItemNotExist = "Product by this id is not exist";
    }

    public static class Guitar
    {
        public const string IncorrectGuitar = "The information about the guitar is not filled correctly!";
    }

    public static class ErrorMessages
    {
        public const string ServerError = "An unexpected problem has occurred. Try again later!";
    }

    public static class HttpClient
    {
        public const string MediaType = "application/json";
    }

    public static class Routes
    {
        public const string AddCartItem = "carts/add-product";
        public const string GetCart = "carts?email={0}";
        public const string Increment = "carts/increment-product";
        public const string Decrement = "carts/decrement-product";
        public const string DeleteCartItem = "carts/delete-product";
        public const string Order = "carts/make-order";
        public const string GetGuitars = "guitars?page-number={0}";
        public const string GetGuitar = "guitars/{0}";
        public const string AddGuitar = "guitars/add";
        public const string UpdateGuitar = "guitars/{0}/update";
        public const string DeleteGuitar = "guitars/{0}/delete";
        public const string Admins = "accounts/admins?page-number={0}";
        public const string Users = "accounts/users?page-number={0}";
        public const string Login = "accounts/login";
        public const string Register = "accounts/register";
        public const string MakeAdmin = "accounts/make-admin";
        public const string MakeUser = "accounts/make-user";
    }
}