
using System;

namespace Elmah.Core.Models
{
    public class Error : BaseModel
    {
        public virtual string Application { get; set; }
        public virtual string Host { get; set; }
        public virtual string Type { get; set; }
        public virtual string Source { get; set; }
        public virtual string Message { get; set; }
        public virtual string User { get; set; }
        public virtual int StatusCode { get; set; }
        public virtual DateTime TimeUtc { get; set; }
        public virtual int Sequence { get; set; }
        public virtual string AllXml { get; set; }
    }
}
