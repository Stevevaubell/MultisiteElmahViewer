using Elmah.Core.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Elmah.Web.IOC
{
    public class SessionSetup
    {
        private readonly IPersistenceConfigurer _persistenceConfigurer;
        private SchemaExport _schemaExport;

        public SessionSetup(string connectionString)
        {
            _persistenceConfigurer = MsSqlConfiguration.MsSql2008
                                    .ConnectionString(connectionString)
                                    .AdoNetBatchSize(10);
        }

        public SessionSetup(IPersistenceConfigurer persistenceConfigurer)
        {
            _persistenceConfigurer = persistenceConfigurer;
        }

        public ISessionFactory GetSessionFactory()
        {
            return Fluently.Configure()
                            .Database(_persistenceConfigurer)
                            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Error>())
                            .BuildSessionFactory();
        }

        public void BuildSchema(ISession session)
        {
            _schemaExport.Execute(true, true, false, session.Connection, null);
        }
    }
}