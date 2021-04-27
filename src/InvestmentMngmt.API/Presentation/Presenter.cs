using InvestmentMngmt.Application;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InvestmentMngmt.API.Presentation
{
    /// <summary>
    /// 
    /// </summary>
    public class Presenter: IPresenter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public IActionResult GetActionResult<T>(Response<T> response)
            => !response.IsValid ? CreateErrorResult(response) : new OkObjectResult(response.Data);

        private static IActionResult CreateErrorResult<T>(Response<T> response)
        {
            var errorBody = new HttpResponseError(response.ErrorMessage, response.Notifications);

            return response.StatusCode switch
            {
                HttpStatusCode.NotFound => new NotFoundObjectResult(errorBody),
                HttpStatusCode.UnprocessableEntity => new UnprocessableEntityObjectResult(errorBody),
                HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(errorBody),
                _ => new BadRequestObjectResult(errorBody),
            };
        }
    }
}
