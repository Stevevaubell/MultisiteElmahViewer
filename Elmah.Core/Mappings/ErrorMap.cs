
using FluentNHibernate.Mapping;

namespace Elmah.Core.Mappings
{
    public class ErrorMap : ClassMap<Models.Error>
    {
        public ErrorMap()
        {
            Table("ELMAH_Error");

            Id(x => x.Id).Column("ErrorId");

            Map(x => x.Application);
            Map(x => x.Host);
            Map(x => x.Type);
            Map(x => x.Source);
            Map(x => x.Message);
            Map(x => x.User).Column("[User]");
            Map(x => x.StatusCode);
            Map(x => x.TimeUtc);
            Map(x => x.Sequence);
            Map(x => x.AllXml);
        }
    }
}
