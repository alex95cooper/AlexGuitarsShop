using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Helpers;

public class ActionResultMaker : Controller
{
    private const string ServerError = "Internal Server Error";

    public ActionResult ResolveResult<T>(IResult<T> resultDto)
    {
        return resultDto.StatusCode switch
        {
            HttpStatusCode.OK => Ok(ResultDtoCreator.GetValidResult(resultDto.Data)),
            HttpStatusCode.NoContent => Ok(ResultDtoCreator.GetValidResult(resultDto.Data)),
            HttpStatusCode.BadRequest => BadRequest(ResultDtoCreator.GetInvalidResult<T>(resultDto.Error)),
            HttpStatusCode.NotFound => NotFound(ResultDtoCreator.GetInvalidResult<T>(resultDto.Error)),
            _ => throw new Exception(ServerError)
        };
    }
}