
using Elmah.Core.Models;
using Elmah.Core.Services;
using Elmah.Service.Tasks.Impl;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace Elmah.Service.Tests.Tasks
{
    public class CleanUpTaskTests
    {
        private CleanUpTask _task;
        private Mock<IDataService> _service;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<IDataService>();
            _task = new CleanUpTask();
            _task.DataService = _service.Object;


            _service.Setup(x => x.GetAll<ElmahSettings>())
                .Returns(Builder<ElmahSettings>.CreateListOfSize(1)
                        .All()
                        .With(x => x.LengthToKeepResults, 1)
                .Build());
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
            _service.Setup(x => x.Find(It.IsAny<Expression<Func<Core.Models.Error, bool>>>())).Returns(new List<Error>());

            _task.RunTask();

            _service.Verify(x => x.Find(It.IsAny<Expression<Func<Core.Models.Error, bool>>>()));
        }

        [Test]
        [ExpectedException(typeof(InvalidDataException))]
        public void No_Settings_Throws_Error()
        {
            _service.Setup(x => x.GetAll<ElmahSettings>()).Returns(new List<ElmahSettings>());

            _task.RunTask();
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
