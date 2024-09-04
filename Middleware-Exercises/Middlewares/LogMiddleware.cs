namespace Middleware_Exercises.Middlewares
{
	public class LogMiddleware
	{
		RequestDelegate _next;

		public LogMiddleware(RequestDelegate next)
		{
			_next = next;
		}
		public async Task Invoke(HttpContext context)
		{
			Console.WriteLine("LogMiddleware: Before");
			throw new Exception(message: "Test 2");
			//await _next(context);
			//Console.WriteLine("LogMiddleware: After");
		}
	}
}
