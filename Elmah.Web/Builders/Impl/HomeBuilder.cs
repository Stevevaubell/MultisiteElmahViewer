
using Elmah.Core.Models;
using Elmah.Core.Services;
using Elmah.Web.Models;

namespace Elmah.Web.Builders.Impl
{
    public class HomeBuilder : IHomeBuilder
    {
        public IDataService DataService { get; set; }

        public HomeViewData Build()
        {
            HomeViewData model = new HomeViewData();
            model.Applications = DataService.GetAll<Application>();

            return model;
        }
    }
}