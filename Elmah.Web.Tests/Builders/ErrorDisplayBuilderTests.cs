
using Elmah.Core.Models;
using Elmah.Core.Services;
using Elmah.Web.Builders.Impl;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Elmah.Web.Tests.Builders
{
    public class ErrorDisplayBuilderTests
    {
        private Mock<IDataService> _service;
        private ErrorDisplayBuilder _builder;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<IDataService>();
            _builder = new ErrorDisplayBuilder()
            {
                DataService = _service.Object
            };
        }

        [Test]
        public void Builder_Loads_Error_For_Application()
        {
            //Arrange
            _service.Setup(x => x.GetAll<Application>()).Returns(new List<Application>());
            _service.Setup(x => x.Find<Error>(It.IsAny<SearchCriteria<Error>>())).Returns(new SearchResult<Error>(){ ResultsList = new Error[0]});

            //Act
            _builder.Build(string.Empty);

            //Assert
            _service.Verify(x => x.Find<Error>(It.IsAny<SearchCriteria<Error>>()));
        }
    }
}
