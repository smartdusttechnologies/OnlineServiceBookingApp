﻿namespace ServiceBooking.Web.UI.Common
{
    /// <summary>
    /// Handling Exceptions
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// This method get Called When Any Execption is Invoked
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        /// <summary>
        /// Handle the exception of this pipeline and redirect to ruled url with message
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var message = exception.Message;
            switch (exception)
            {
                //We need to extend Cases like to handle accurate Exceptions
                case UnauthorizedAccessException ex:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    break;
                default:
                    context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
                    message = "Unable To Process Your Request  Please Contact To Admin";
                    break;
            }
            // Return an error message as JSON
            var errorResponse = new { Message = message };
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
