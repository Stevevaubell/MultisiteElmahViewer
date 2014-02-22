
using System.Collections.Generic;

namespace Elmah.Core.Models
{
    public class SearchResult<T> where T : BaseModel
    {
        public int TotalResults { get; set; }
        public IList<T> ResultsList { get; set; }
    }
}
