using DevLegends.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevLegends.API.Controllers
{
	[ApiController]
	[Route("/test")]
	public class TestController : ControllerBase
	{
		private readonly ITestService _testService;
		public TestController(ITestService testService)
		{
			_testService = testService;
		}


		[HttpGet]
		public void Do()
		{
			_testService.Do();
		}
	}
}
