
using Elmah.Core.Models;
using Elmah.Core.Services;
using Elmah.Core.Util;
using Elmah.Service.Tasks.Impl;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Elmah.Service.Tests.Tasks
{
    public class FindApplicationsTaskTests
    {
        private FindApplicationsTask _task;
        private Mock<IDataService> _service;
        private Mock<IAppSettingsHelper> _helper;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<IDataService>();
            _helper = new Mock<IAppSettingsHelper>();
            _task = new FindApplicationsTask();
            _task.DataService = _service.Object;
            _task.AppSettingsHelper = _helper.Object;

            Application application = Builder<Application>.CreateNew().Build();
            _service.Setup(x => x.GetAll<Application>())
                .Returns(new List<Application>() { application });

            _service.Setup(x => x.FindDistinct<Error>(It.IsAny<string>()))
                .Returns(Builder<Error>.CreateListOfSize(1)
                    .All()
                    .With(x => x.Application, "New One")
                    .Build());

            _service.Setup(x => x.Save(It.IsAny<Application>()));

            _helper.Setup(x => x.GetIntConfig(It.IsAny<string>())).Returns(1);
        }

        [Test]
        public void Task_Loads_Applications()
        {
            _task.RunTask();

            _service.Verify(x => x.GetAll<Application>());
        }

        [Test]
        public void Task_Adds_Application()
        {
            _task.RunTask();
            _service.Verify(x => x.Save(It.IsAny<Application>()));
        }
    }
}
