
using System.Web.Mvc;
using Elmah.Web.Builders;
using Elmah.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace Elmah.Web.Tests.Controllers
{
    public class ErrorDisplayControllerTests
    {
        private Mock<IErrorDisplayBuilder> _builder;
        private ErrorDisplayController _controller;

        [SetUp]
        public void Setup()
        {
            _builder = new Mock<IErrorDisplayBuilder>();
            _controller = new ErrorDisplayController()
            {
                ErrorDisplayBuilder = _builder.Object
            };
        }

        [Test]
        public void Controller_Calls_Builder_To_Load_Item()
        {
            //Arrange
            _builder.Setup(x => x.Build(It.IsAny<string>()));

            //Act
            _controller.Index("Application");

            //Assert
            _builder.Verify(x => x.Build(It.IsAny<string>()));
        }

        [Test]
        public void Controller_Redirects_On_Empty_Params()
        {
            //Act
            ActionResult result = _controller.Index(null);

            //Assert
            Assert.IsNotNull(result as RedirectToRouteResult);
        }
    }
}
