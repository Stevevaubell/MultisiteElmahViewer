
using System;
using Elmah.Service.Tasks;
using Elmah.Service.Util;
using Moq;
using NUnit.Framework;

namespace Elmah.Service.Tests
{
    public class ServiceTests
    {
        private Mock<IErrorHelper> _errorHelper;
        private Mock<ICleanUpTask> _cleanUpTask;
        private Mock<IFindApplicationsTask> _findApplicationsTask;
        private Service _service;
    
        [SetUp]
        public void Setup()
        {
            _errorHelper = new Mock<IErrorHelper>();
            _cleanUpTask = new Mock<ICleanUpTask>();
            _findApplicationsTask = new Mock<IFindApplicationsTask>();
            _service = new Service()
            {
                ErrorHelper = _errorHelper.Object,
                CleanUpTask = _cleanUpTask.Object,
                FindApplicationsTask = _findApplicationsTask.Object
            };
        }

        [Test]
        public void Service_Runs_Clean_Task_On_Interval()
        {
            _cleanUpTask.Setup(x => x.RunTask());

            _service.timer_Tick(null, new EventArgs());

            _cleanUpTask.Verify(x => x.RunTask());
        }

        [Test]
        public void Service_Runs_Find_Task_On_Interval()
        {
            _findApplicationsTask.Setup(x => x.RunTask());

            _service.timer_Tick(null, new EventArgs());

            _findApplicationsTask.Verify(x => x.RunTask());
        }
    }
}
