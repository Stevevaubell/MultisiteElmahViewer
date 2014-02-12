
using Elmah.Web.Builders;
using Elmah.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace Elmah.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        private Mock<IHomeBuilder> _builder;
        private HomeController _controller;

        [SetUp]
        public void Setup()
        {
            _builder = new Mock<IHomeBuilder>();
            _controller = new HomeController()
            {
                HomeBuilder = _builder.Object
            };
        }

        [Test]
        public void Controller_Calls_Builder_To_Load_Items()
        {
            //Arrange
            _builder.Setup(x => x.Build());

            //Act
            _controller.Index();

            //Assert
            _builder.Verify(x => x.Build());
        }
    }
}
