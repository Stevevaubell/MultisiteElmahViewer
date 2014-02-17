using System.Web.Mvc;
using Autofac;
using Elmah.Web.Builders.Api;
using Elmah.Web.Models.Api;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using Microsoft.Owin.Logging;

namespace Elmah.Web.Hubs
{
    public class ApplicationStateHub : Hub
    {
        public ApplicationStateTicker _applicationStateTicker;
        readonly ILifetimeScope _hubLifetimeScope; 

        public ApplicationStateHub(ILifetimeScope lifetimeScope)
        {
            // Create a lifetime scope for the hub.
            _hubLifetimeScope = lifetimeScope.BeginLifetimeScope();

            // Resolve dependencies from the hub lifetime scope.
            _applicationStateTicker = _hubLifetimeScope.Resolve<ApplicationStateTicker>();

            

            _applicationStateTicker.SetupApplicationStateTicker(this.Clients);
        }

        public IEnumerable<ApplicationState> Get()
        {
            return _applicationStateTicker.GetStates();
        }

        protected override void Dispose(bool disposing)
        {
            // Dipose the hub lifetime scope when the hub is disposed.
            if (disposing && _hubLifetimeScope != null)
                _hubLifetimeScope.Dispose();

            base.Dispose(disposing);
        }
    }
}

