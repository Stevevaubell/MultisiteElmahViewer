
using Elmah.Web.Builders.Api;
using Elmah.Web.Models.Api;
using Microsoft.AspNet.SignalR.Hubs;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Elmah.Web.Hubs
{
    public class ApplicationStateTicker
    {
        public IApplicationStateApiBuilder ApplicationStateApiBuilder { get; set; }

        private Timer _timer;
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(1000);
        private readonly object _updateStatesLock = new object();
        private volatile bool _updatingStates = false;
        private readonly IList<ApplicationState> _applicationStates = new List<ApplicationState>();

        private IHubConnectionContext Clients { get; set; }


        public void SetupApplicationStateTicker(IHubConnectionContext clients)
        {
            Clients = clients;

            _timer = new Timer(Update, null, _updateInterval, _updateInterval);
            _applicationStates.Clear();
            ApplicationStateApiBuilder.Build().ForEach(state => _applicationStates.Add(state));
        }

        private void Update(object state)
        {
            lock (_updateStatesLock)
            {
                if (!_updatingStates)
                {
                    _updatingStates = true;
                    _applicationStates.Clear();
                    ApplicationStateApiBuilder.Build().ForEach(x => _applicationStates.Add(x));
                    foreach (var applicationState in _applicationStates)
                    {
                        Broadcast(applicationState);
                    }

                    _updatingStates = false;
                }
            }
        }

        private void Broadcast(ApplicationState state)
        {
            Clients.All.update(state);
        }

        internal IEnumerable<ApplicationState> GetStates()
        {
            return _applicationStates;
        }
    }
}