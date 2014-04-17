using System;
using System.Collections.Specialized;
using System.Web;

namespace Elmah.Web.Models
{
    public class OriginalError
    {
        public Exception Exception { get; set; }
        public string ApplicationName { get; set; }
        public string HostName { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public string User { get; set; }
        public DateTime Time { get; set; }
        public int StatusCode { get; set; }
        public string WebHostHtmlMessage { get; set; }
        public NameValueCollection ServerVariables { get; set; }
        public NameValueCollection QueryString { get; set; }
        public NameValueCollection Form { get; set; }
        public NameValueCollection Cookies { get; set; }
    }
}