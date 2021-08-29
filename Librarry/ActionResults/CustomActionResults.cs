using Librarry.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarry.ActionResults
{
    public class CustomActionResults : IActionResult
    {
        private readonly CustomActionResultVM _cusctomResult;

        public CustomActionResults(CustomActionResultVM cusctomResult)
        {
            _cusctomResult = cusctomResult;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_cusctomResult.Exception ?? _cusctomResult.Publisher as object)
            {
                StatusCode = _cusctomResult.Exception != null ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
