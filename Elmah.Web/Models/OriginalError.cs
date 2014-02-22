using System;
using System.Collections.Specialized;
using System.Web;

namespace Elmah.Web.Models
{
    public class OriginalError : ICloneable
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
        
        object ICloneable.Clone()
        {
            //
            // Make a base shallow copy of all the members.
            //

            OriginalError copy = (OriginalError) MemberwiseClone();

            //
            // Now make a deep copy of items that are mutable.
            //

            copy.ServerVariables = CopyCollection(ServerVariables);
            copy.QueryString = CopyCollection(QueryString);
            copy.Form = CopyCollection(Form);
            copy.Cookies = CopyCollection(Cookies);

            return copy;
        }

        private static NameValueCollection CopyCollection(NameValueCollection collection)
        {
            if (collection == null || collection.Count == 0)
                return null;

            return new NameValueCollection(collection);
        }

        private static NameValueCollection CopyCollection(HttpCookieCollection cookies)
        {
            if (cookies == null || cookies.Count == 0)
                return null;

            NameValueCollection copy = new NameValueCollection(cookies.Count);

            for (int i = 0; i < cookies.Count; i++)
            {
                HttpCookie cookie = cookies[i];

                //
                // NOTE: We drop the Path and Domain properties of the 
                // cookie for sake of simplicity.
                //

                copy.Add(cookie.Name, cookie.Value);
            }

            return copy;
        }
    }
}