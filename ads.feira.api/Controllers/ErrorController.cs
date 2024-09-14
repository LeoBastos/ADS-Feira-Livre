using ads.feira.api.Models.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ads.feira.api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public ErrorResponse Error()
        {

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;


            Response.StatusCode = 500;
            var idError = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            return new ErrorResponse(idError, HttpContext?.TraceIdentifier);
        }
    }
}
