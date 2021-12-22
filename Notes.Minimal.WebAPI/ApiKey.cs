﻿namespace Notes.WebAPI.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class ApiKey : Attribute, IAsyncActionFilter
{
	private const string APIKEYNAME = "ApiKey";
	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
		{
			context.Result = new ContentResult()
			{
				StatusCode = 401,
				Content = "Api Key was not provided"
			};
			return;
		}

		var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

		var apiKey = appSettings.GetValue<string>(APIKEYNAME);

		if (!apiKey.Equals(extractedApiKey))
		{
			context.Result = new ContentResult()
			{
				StatusCode = 401,
				Content = "Api Key is not valid"
			};
			return;
		}

		await next();
	}
}

