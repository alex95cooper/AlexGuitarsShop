namespace AlexGuitarsShop.Domain.Interfaces;

public interface IResponse<out T>
{
    string Description { get; }
    StatusCode StatusCode { get; }
    T Data { get; }
}