
using System.Xml;
using Elmah.Web.Models;

namespace Elmah.Web.Util
{
    public interface IErrorXmlHelper
    {
        OriginalError DecodeString(string xml);

        OriginalError Decode(XmlReader reader);

        string EncodeString(OriginalError originalError);

        void Encode(OriginalError originalError, XmlWriter writer);
    }
}
