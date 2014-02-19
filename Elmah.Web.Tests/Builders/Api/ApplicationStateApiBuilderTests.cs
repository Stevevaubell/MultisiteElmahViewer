
using System.Collections.Generic;
using Elmah.Core.Models;
using Elmah.Core.Services;
using Elmah.Web.Builders.Api.Impl;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using System;
using System.Linq.Expressions;

namespace Elmah.Web.Tests.Builders
{
    public class ApplicationStateApiBuilderTests
    {
        private Mock<IDataService> _service;
        private ApplicationStateApiBuilder _builder;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<IDataService>();
            _builder = new ApplicationStateApiBuilder()
            {
                DataService = _service.Object
            };
        }

        [Test]
        public void Builder_Loads_Errors()
        {
            //Arrange
            _service.Setup(x => x.GetAll<Application>()).Returns(new List<Application>());
            _service.Setup(x => x.Find<Error>(It.IsAny<Expression<Func<Error, bool>>>())).Returns(Builder<Error>.CreateListOfSize(1).Build());

            //Act
            _builder.Build();

            //Assert
            _service.Verify(x => x.Find<Error>(It.IsAny<Expression<Func<Error, bool>>>()));
        }
    }
}
