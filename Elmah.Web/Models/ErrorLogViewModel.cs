using Elmah.Core.Models;
using System.Collections.Generic;

namespace Elmah.Web.Models
{
    public class ErrorLogViewModel
    {
        public int PageNumber { get; set; }
        public int TotalPage { get; set; }
        public string HostName { get; set; }
        public string ApplicationName { get; set; }
        public IList<Error> Errors { get; set; }
    }
}