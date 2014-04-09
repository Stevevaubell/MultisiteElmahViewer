
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
    public class CleanUpTaskTests
    {
        private CleanUpTask _task;
        private Mock<IDataService> _service;
        private Mock<IAppSettingsHelper> _helper;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<IDataService>();
            _helper = new Mock<IAppSettingsHelper>();
            _task = new CleanUpTask();
            _task.DataService = _service.Object;
            _task.AppSettingsHelper = _helper.Object;

            Application application = Builder<Application>.CreateNew().Build();
            _service.Setup(x => x.GetAll<Application>())
                .Returns(new List<Application>() { application });

            _service.Setup(x => x.GetAll<ElmahSettings>())
                .Returns(Builder<ElmahSettings>.CreateListOfSize(1)
                        .All()
                        .With(x => x.Application, application)
                        .With(x => x.LengthToKeepResults, 1)
                .Build());

            _helper.Setup(x => x.GetIntConfig(It.IsAny<string>())).Returns(1);

            _service.Setup(x => x.Find(It.IsAny<Expression<Func<Core.Models.Error, bool>>>())).Returns(new List<Error>());
        }

        [Test]
        public void Task_Loads_Settings()
        {
            _service.Setup(x => x.Find(It.IsAny<Expression<Func<Core.Models.Error, bool>>>())).Returns(new List<Error>() { new Error() });
            _task.RunTask();

            _service.Verify(x => x.GetAll<ElmahSettings>());
        }

        [Test]
        public void Task_Loads_Errors_Older_Than_Setting()
        {

            _task.RunTask();

            _service.Verify(x => x.Find(It.IsAny<Expression<Func<Core.Models.Error, bool>>>()));
        }

        [Test]
        public void No_Settings_Creates_New()
        {
            _service.Setup(x => x.GetAll<ElmahSettings>()).Returns(new List<ElmahSettings>());
            _service.Setup(x => x.Save(It.IsAny<ElmahSettings>()));

            _task.RunTask();

            _service.Verify(x => x.Save(It.IsAny<ElmahSettings>()));
        }

        [Test]
        public void Task_Deletes_Errors_Older_Than_Setting()
        {
            _service.Setup(x => x.Find(It.IsAny<Expression<Func<Core.Models.Error, bool>>>())).Returns(new List<Error>() { new Error() });
            _service.Setup(x => x.Delete<Error>(It.IsAny<Guid>()));

            _task.RunTask();

            _service.Verify(x => x.Delete<Error>(It.IsAny<Guid>()));
        }
    }
}
