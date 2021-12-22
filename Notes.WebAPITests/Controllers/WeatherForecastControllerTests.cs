using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Notes.WebAPI.Controllers.Tests
{
    [TestClass()]
    public class WeatherForecastControllerTests
    {
        private WeatherForecastController weatherForecastController;
        private Mock<HttpContext> httpContext;

        [TestInitialize()]
        public void Initialize()
        {
            var mockLogger = new Mock<ILogger<WeatherForecastController>>();
            weatherForecastController = new WeatherForecastController(mockLogger.Object);
            httpContext = new Mock<HttpContext>();
            weatherForecastController.ControllerContext = new ControllerContext();
            weatherForecastController.ControllerContext.HttpContext = httpContext.Object;
        }

        [TestMethod()]
        public void GetRequestTest()
        {
            var result = weatherForecastController.Get();
            //test
            Assert.IsTrue(result?.Count() > 0);
        }
    }
}