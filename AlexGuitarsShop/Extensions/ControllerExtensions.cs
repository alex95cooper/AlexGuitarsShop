using System.Net;
using AlexGuitarsShop.Common;
using AlexGuitarsShop.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Extensions;

public static class ControllerExtensions
{
    private const string ServerError = "Internal Server Error";

    public static ActionResult ResolveResult<T>(this Controller controller, IResult<T> result)
    {
        return result.StatusCode switch
        {
            HttpStatusCode.OK => controller.Ok(ResultDtoCreator.GetValidResult(result.Data)),
            HttpStatusCode.NoContent => controller.Ok(ResultDtoCreator.GetValidResult(result.Data)),
            HttpStatusCode.BadRequest => controller.BadRequest(ResultDtoCreator.GetInvalidResult<T>(result.Error)),
            HttpStatusCode.NotFound => controller.NotFound(ResultDtoCreator.GetInvalidResult<T>(result.Error)),
            _ => throw new Exception(ServerError)
        };
    }

    public static ActionResult ResolveResult(this Controller controller, Domain.IResult result)
    {
        return result.StatusCode switch
        {
            HttpStatusCode.OK => controller.Ok(ResultDtoCreator.GetValidResult()),
            HttpStatusCode.NoContent => controller.Ok(ResultDtoCreator.GetValidResult()),
            HttpStatusCode.BadRequest => controller.BadRequest(ResultDtoCreator.GetInvalidResult(result.Error)),
            HttpStatusCode.NotFound => controller.NotFound(ResultDtoCreator.GetInvalidResult(result.Error)),
            _ => throw new Exception(ServerError)
        };
    }
}