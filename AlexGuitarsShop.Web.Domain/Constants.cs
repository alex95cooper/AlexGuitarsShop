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
        public const string AddCartItem = "cart/status-action=add";
        public const string GetCart = "cart/index/{0}";
        public const string Increment = "cart/status-action=increment-cart-item";
        public const string Decrement = "cart/status-action=decrement-cart-item";
        public const string DeleteCartItem = "cart/id={0}&email={1}/status-action=delete-cart-item";
        public const string Order = "cart/order/{0}";
        public const string GetGuitars = "guitar/index/{0}";
        public const string GetGuitar = "guitar/{0}";
        public const string AddGuitar = "guitar/status-action=add";
        public const string UpdateGuitar = "guitar/status-action=update-guitar";  
        public const string DeleteGuitar = "guitar/{0}/status-action=delete-guitar";
        public const string Admins = "account/admins/{0}";
        public const string Users = "account/users/{0}";
        public const string Login = "account/login";  
        public const string Register = "account/register";  
        public const string MakeAdmin = "account/status-action=make-admin"; 
        public const string MakeUser = "account/status-action=make-user";  
    }
}