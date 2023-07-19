using AlexGuitarsShop.Domain.Enums;

namespace AlexGuitarsShop.Domain.Interfaces;

public interface IResponse<T>
{
    string Description { get; }
    StatusCode StatusCode { get; }
    T Data { get; }
}