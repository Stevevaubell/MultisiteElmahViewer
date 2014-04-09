
using FluentNHibernate.Mapping;

namespace Elmah.Core.Mappings
{
    public class ElmahSettingsMap : ClassMap<Models.ElmahSettings>
    {
        public ElmahSettingsMap()
        {
            Table("Application");

            Id(x => x.Id).Column("Id");
            References(x => x.Application).Column("ApplicationId");
            Map(x => x.LengthToKeepResults);
        }
    }
}
