namespace INDWalks.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await next(httpcontext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                
                //log this exception
                logger.LogError(ex, $"{errorId}:{ex.Message}");

                //return generic error response to client
                httpcontext.Response.StatusCode = 500; // Internal Server Error
                httpcontext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong."
                };
                await httpcontext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
