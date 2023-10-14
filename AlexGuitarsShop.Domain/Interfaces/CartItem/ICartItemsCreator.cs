namespace AlexGuitarsShop.Domain.Interfaces.CartItem;

public interface ICartItemsCreator
{
    Task<IResult> AddNewCartItemAsync(DAL.Models.Guitar guitar, int accountId);
}