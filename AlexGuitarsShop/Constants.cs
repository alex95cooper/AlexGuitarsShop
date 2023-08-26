namespace AlexGuitarsShop;

public static class Constants
{
    public static class Roles
    {
        public const string AdminPlus = "SuperAdmin, Admin";
        public const string MainRole = "SuperAdmin";
        public const string StandardRole = "User";
    }

    public static class ErrorMessages
    {
        public const string InvalidAccount = "The information about the account is not filled correctly!";
        public const string InvalidEmail = "Entered Email is not exists!";
        public const string InvalidGuitar = "The information about the account is not filled correctly!";
        public const string InvalidGuitarId = "Guitar by this id don't exist!";
        public const string InvalidProductId = "Item by this id don't exist in cart!";
        public const string InvalidPage = "Entered number of page don't exist!";
        
    }
}