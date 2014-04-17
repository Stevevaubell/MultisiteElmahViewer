
using Elmah.Web.Models.Api;
using Elmah.Web.Util;
using Elmah.Web.Util.Impl;
using Moq;
using NUnit.Framework;

namespace Elmah.Web.Tests.Models.Api
{
    public class ApplicationStateTests
    {
        Mock<IConfigSettings> _mock = new Mock<IConfigSettings>();
        ApplicationState _state = new ApplicationState();

        [SetUp]
        public void Setup()
        {
            _state.Settings = _mock.Object;

            _mock.Setup(x => x.AppSettings(ConfigSettings.OkCount)).Returns("1");
            _mock.Setup(x => x.AppSettings(ConfigSettings.WarnCount)).Returns("2");
            _mock.Setup(x => x.AppSettings(ConfigSettings.ErrorCount)).Returns("3");
        }

        [Test]
        public void Class_Returns_Correct_danger()
        {
            _state.ErrorCount = 5;

            var css = _state.Class;

            Assert.AreSame(css, ApplicationState.ERRORCLASS);
        }

        [Test]
        public void Class_Returns_Correct_Warning()
        {
            _state.ErrorCount = 2;

            var css = _state.Class;

            Assert.AreSame(css, ApplicationState.WARNCLASS);
        }

        [Test]
        public void Class_Returns_Correct_Ok()
        {
            _state.ErrorCount = 0;

            var css = _state.Class;

            Assert.AreSame(css, ApplicationState.OKCLASS);
        }
    }
}
