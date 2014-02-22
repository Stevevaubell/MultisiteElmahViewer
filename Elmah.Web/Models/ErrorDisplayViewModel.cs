using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using Elmah.Core.Models;

namespace Elmah.Web.Models
{
    public class ErrorDisplayViewModel
    {
        public Error Error { get; set; }
        public OriginalError OriginalError { get; set; }

        public readonly Regex StackTrace = new Regex(@"
                ^
                \s*
                \w+ \s+ 
                (?<type> .+ ) \.
                (?<method> .+? ) 
                (?<params> \( (?<params> .*? ) \) )
                ( \s+ 
                \w+ \s+ 
                  (?<file> [a-z] \: .+? ) 
                  \: \w+ \s+ 
                  (?<line> [0-9]+ ) \p{P}? )?
                \s*
                $",
            RegexOptions.IgnoreCase
            | RegexOptions.Multiline
            | RegexOptions.ExplicitCapture
            | RegexOptions.CultureInvariant
            | RegexOptions.IgnorePatternWhitespace
            | RegexOptions.Compiled);
    }
}