using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Moq;
using Microsoft.AspNetCore.Http;
using Notes.WebAPITests.TestData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Notes.WebAPI.Controllers.Tests
{
    [TestClass()]
    public class NotesAPIControllerTests
    {
        private NotesAPIController controller;
        private Mock<HttpContext> httpContext;
        private Mock<ILogger<NotesAPIController>> mockLogger;

        [TestInitialize()]
        public void Initialize()
        {
            mockLogger = new Mock<ILogger<NotesAPIController>>();
            var httpResponse = new Mock<HttpResponse>();
            controller = new NotesAPIController(new TestRepo(), mockLogger.Object);
            httpContext = new Mock<HttpContext>();
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = httpContext.Object;
            _ = httpContext.Setup(m => m.Response).Returns(httpResponse.Object);
        }

        [TestMethod()]
        public void GetRequestTest()
        {
            var result = controller.Get();

            Assert.IsTrue(result?.Count() > 0);
        }
    }
}
