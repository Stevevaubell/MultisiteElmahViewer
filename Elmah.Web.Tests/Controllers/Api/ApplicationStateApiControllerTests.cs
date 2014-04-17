
using Elmah.Web.Builders.Api;
using Elmah.Web.Controllers.Api;
using Moq;
using NUnit.Framework;

namespace Elmah.Web.Tests.Controllers.Api
{
    public class ApplicationStateApiControllerTests
    {
        private ApplicationStateApiController _apiController;
        public Mock<IApplicationStateApiBuilder> _builder;

        [SetUp]
        public void Setup()
        {
            _builder = new Mock<IApplicationStateApiBuilder>();
            _apiController = new ApplicationStateApiController();
            _apiController.ApplicationStateApiBuilder = _builder.Object;
        }

        [Test]
        public void Controller_Calls_Builder()
        {
            _builder.Setup(x => x.Build());

            _apiController.Get();

            _builder.Verify(x => x.Build());
        }
    }
}
