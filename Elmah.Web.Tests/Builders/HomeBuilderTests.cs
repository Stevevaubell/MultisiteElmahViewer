
using Elmah.Core.Models;
using Elmah.Core.Services;
using Elmah.Web.Builders.Impl;
using Moq;
using NUnit.Framework;

namespace Elmah.Web.Tests.Builders
{
    public class HomeBuilderTests
    {
        private Mock<IDataService> _service;
        private HomeBuilder _builder;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<IDataService>();
            _builder = new HomeBuilder()
            {
                DataService = _service.Object
            };
        }

        [Test]
        public void Builder_Loads_Applications()
        {
            //Arrange
            _service.Setup(x => x.GetAll<Application>());

            //Act
            _builder.Build();

            //Assert
            _service.Verify(x => x.GetAll<Application>());
        }
    }
}
