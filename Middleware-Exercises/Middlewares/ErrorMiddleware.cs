using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace Middleware_Exercises.Middlewares
{
	public class ErrorMiddleware
	{
		private readonly RequestDelegate _next;
		private ILogger<ErrorMiddleware> _logger;

		public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}
		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				_logger.LogError(e, "ErrorMiddleware:Error");
				await HandleExceptionAsync(context, e);
			}
			
		}
		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			var entity=new
			{
				StatusCode = context.Response.StatusCode,
				Message = exception.Message
			};

			return context.Response.WriteAsync(JsonConvert.SerializeObject(entity));
		}

	}
}
