namespace AlexGuitarsShop.Domain;

public static class Constants
{
    public static class Account
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 20;
        public const int PasswordMinLength = 5;
        public const int PasswordMaxLength = 50;
        public const string EmailPattern = @"^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$";
    }

    public static class Guitar
    {
        public const int MinPrice = 10;
        public const int MaxPrice = 1000000;
        public const int NameMinLength = 5;
        public const int NameMaxLength = 50;
        public const int DescriptionMinLength = 10;
        public const int DescriptionMaxLength = 600;
    }

    public static class ErrorMessages
    {
        public const string InvalidAccount = "The information about the account is not filled correctly!";
        public const string InvalidEmail = "Entered Email is not exists!";
        public const string InvalidGuitar = "The information about the guitar is not filled correctly!";
        public const string InvalidGuitarId = "Guitar by this id don't exist!";
        public const string UserNotFound = "User is not found";
        public const string InvalidPassword = "Invalid password or login";
    }
}