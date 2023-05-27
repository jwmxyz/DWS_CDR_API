using Cdr.Api.Pipeline.Filters;
using Cdr.ErrorManagement;
using Cdr.ErrorManagement.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cdr.Api.Tests
{
    [TestClass]
    public class CdrExceptionFilterTest
    {
        private Mock<ILogger<CdrErrorManager>> _loggerMock;


        [TestMethod]
        public void ExceptionThrown_WhenExceptionThrown_500MaskedResponse()
        {
            _loggerMock = new Mock<ILogger<CdrErrorManager>>();
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider
                .Setup(x => x.GetService(typeof(ICdrErrorManager)))
                .Returns(new CdrErrorManager(_loggerMock.Object));

            serviceProvider
                .Setup(x => x.GetService(typeof(ILogger<CdrExceptionFilter>)))
                .Returns(Mock.Of<ILogger<CdrExceptionFilter>>());

            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    RequestServices = serviceProvider.Object,
                },
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };

            var mockException = new Mock<Exception>();

            mockException.Setup(e => e.StackTrace)
              .Returns("Test stacktrace");
            mockException.Setup(e => e.Message)
              .Returns("Test message");
            mockException.Setup(e => e.Source)
              .Returns("Test source");

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = mockException.Object
            };

            var filter = new CdrExceptionFilter();

            filter.OnException(exceptionContext);
            Assert.AreEqual(500, exceptionContext.Result.GetType()
                                        .GetProperty("StatusCode")
                                        .GetValue(exceptionContext.Result, null));
        }


        [TestMethod]
        public void ExceptionThrown_WhenInvalidCSVFileExceptionThrown_400MaskedResponse()
        {
            _loggerMock = new Mock<ILogger<CdrErrorManager>>();
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider
                .Setup(x => x.GetService(typeof(ICdrErrorManager)))
                .Returns(new CdrErrorManager(_loggerMock.Object));

            serviceProvider
                .Setup(x => x.GetService(typeof(ILogger<CdrExceptionFilter>)))
                .Returns(Mock.Of<ILogger<CdrExceptionFilter>>());

            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    RequestServices = serviceProvider.Object,
                },
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };

            var mockException = new InvalidCsvException();

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = mockException,
            };

            var filter = new CdrExceptionFilter();

            filter.OnException(exceptionContext);
            Assert.AreEqual(400, exceptionContext.Result.GetType()
                                        .GetProperty("StatusCode")
                                        .GetValue(exceptionContext.Result, null));
        }
    }
}
