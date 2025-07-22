using System.Net;
using CarStore.Communication.Response;
using CarStore.Exceptions;
using CarStore.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarStore.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is CarStoreException carStoreException)
            {
                HandleProjectException(carStoreException, context);
            }
            else
            {
                ThrowUnknowException(context);
            }
        }

        private static void HandleProjectException(CarStoreException carStoreException, ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)carStoreException.GetStatusCode();
            context.Result = new ObjectResult(new ResponseErrorJson(carStoreException.GetErrorMessages()));
        }
        private static void ThrowUnknowException(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidationException exception)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
            }

        }

    }
}
