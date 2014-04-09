
using System;

namespace Elmah.Core.Models
{
    public class ElmahSettings : BaseModel
    {
        public virtual Application Application { get; set; }
        public virtual int LengthToKeepResults { get; set; }
    }
}
