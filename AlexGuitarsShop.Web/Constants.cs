namespace AlexGuitarsShop.Web;

public static class Constants
{
    public static class Roles
    {
        public const string AdminPlus = "SuperAdmin, Admin";
        public const string SuperAdmin = "SuperAdmin";
    }

    public static class ErrorMessages
    {
        public const string InvalidAccount = "The information about the account is not filled correctly!";
        public const string InvalidGuitarId = "Guitar by this id don't exist!";
        public const string CatalogEmpty = "Catalog is empty!";
        public const string CartEmpty = "Cart is empty!";
        public const string NoAdmins = "No admins!";
        public const string NoUsers = "No users!";
    }
}