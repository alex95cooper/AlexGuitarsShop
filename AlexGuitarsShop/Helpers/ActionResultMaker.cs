using System.Net;
using AlexGuitarsShop.Common;
using Microsoft.AspNetCore.Mvc;

namespace AlexGuitarsShop.Helpers;

public class ActionResultMaker : Controller
{
    private const string ServerError = "Internal Server Error";

    public ActionResult ResolveResult<T>(IResult<T> result)
    {
        return result.StatusCode switch
        {
            HttpStatusCode.OK => Ok(ResultCreator.GetValidResult(result.Data, HttpStatusCode.OK)),
            HttpStatusCode.NoContent => Ok(ResultCreator.GetValidResult(result.Data, HttpStatusCode.NoContent)),
            HttpStatusCode.BadRequest => BadRequest(
                ResultCreator.GetInvalidResult<T>(result.Error, HttpStatusCode.BadRequest)),
            HttpStatusCode.NotFound => BadRequest(
                ResultCreator.GetInvalidResult<T>(result.Error, HttpStatusCode.NotFound)),
            HttpStatusCode.InternalServerError => BadRequest(
                ResultCreator.GetInvalidResult<T>(result.Error, HttpStatusCode.InternalServerError)),
            _ => throw new Exception(ServerError)
        };
    }
}