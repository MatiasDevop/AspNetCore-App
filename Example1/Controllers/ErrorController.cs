using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Example1.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logs;
        ErrorController(ILogger<ErrorController> log)
        {
            this.logs = log;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "The Resource dosent Exist";
                    break;
            }
            return View("Error");
        }
        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            //ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            //ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;

            logs.LogError($"Route from error: {exceptionHandlerPathFeature.Path}" +
                $"Exception: {exceptionHandlerPathFeature.Error}" +
                $"Traza from Error : {exceptionHandlerPathFeature.Error.StackTrace}");

            return View("ErrorGeneric");
        }
    }
}