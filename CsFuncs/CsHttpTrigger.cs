using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.Function;

public class CsHttpTrigger(ILogger<CsHttpTrigger> logger)
{
	private readonly ILogger<CsHttpTrigger> _logger = logger;

	[Function("CsHttpTrigger")]
	public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
	{
		_logger.LogInformation("C# HTTP trigger function processed a request.");
		return new OkObjectResult("Welcome to Azure Functions!");
	}
}
