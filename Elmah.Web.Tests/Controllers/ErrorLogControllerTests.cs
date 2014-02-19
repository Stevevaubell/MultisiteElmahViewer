
using Elmah.Web.Builders;
using Elmah.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace Elmah.Web.Tests.Controllers
{
    public class ErrorLogControllerTests
    {
        private Mock<IErrorLogBuilder> _builder;
        private ErrorLogController _controller;

        [SetUp]
        public void Setup()
        {
            _builder = new Mock<IErrorLogBuilder>();
            _controller = new ErrorLogController()
            {
                ErrorLogBuilder = _builder.Object
            };
        }

        [Test]
        public void Controller_Calls_Builder_To_Load_Items()
        {
            //Arrange
            _builder.Setup(x => x.Build(It.IsAny<string>(), It.IsAny<int>()));

            //Act
            _controller.Index(string.Empty, 1);

            //Assert
            _builder.Verify(x => x.Build(It.IsAny<string>(), It.IsAny<int>()));
        }
    }
}
