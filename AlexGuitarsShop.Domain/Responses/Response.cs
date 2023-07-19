using AlexGuitarsShop.Domain.Enums;
using AlexGuitarsShop.Domain.Interfaces;

namespace AlexGuitarsShop.Domain.Responses;

public class Response<T>: IResponse<T>
{
    public string Description { get; set; }
    public StatusCode StatusCode { get; set; }
    public T Data { get; set; }
}

