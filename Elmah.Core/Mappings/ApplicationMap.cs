
using FluentNHibernate.Mapping;

namespace Elmah.Core.Mappings
{
    public class ApplicationMap : ClassMap<Models.Application>
    {
        public ApplicationMap()
        {
            Table("Application");

            Id(x => x.Id).Column("ErrorId");
            Map(x => x.Name);
        }
    }
}
