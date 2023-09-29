namespace AlexGuitarsShop;

public static class Constants
{
    public static class ErrorMessages
    {
        public const string InvalidEmail = "Entered Email is not exists!";
        public const string InvalidGuitarId = "Guitar by this id don't exist!";
        public const string InvalidPage = "Entered number of page don't exist!";
    }
    
    public static class Routes
    {
        public const string Add = "status-action=add";
        public const string GetCart = "index/{email}";
        public const string Increment = "status-action=increment-cart-item";
        public const string Decrement = "status-action=decrement-cart-item";
        public const string DeleteCartItem = "id={id}&email={email}/status-action=delete-cart-item";
        public const string Order = "order/{email}";
        public const string GetGuitars = "index/{pageNumber}";
        public const string GetGuitar = "{id}";
        public const string UpdateGuitar = "status-action=update-guitar";  
        public const string DeleteGuitar = "{id}/status-action=delete-guitar";
        public const string Admins = "admins/{pageNumber}";
        public const string Users = "users/{pageNumber}";
        public const string Login = "login";  
        public const string Register = "register";  
        public const string MakeAdmin = "status-action=make-admin"; 
        public const string MakeUser = "status-action=make-user";  
    }
}