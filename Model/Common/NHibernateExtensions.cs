using Microsoft.Extensions.DependencyInjection;
using Model.Common;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Common
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            //var mapper = new ModelMapper();
            //mapper.AddMappings(typeof(NHibernateExtensions).Assembly.ExportedTypes);
            //HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = new Configuration();
            configuration.DataBaseIntegration(c =>
            {
                c.Dialect<Oracle12cDialect>();
                c.Driver<OracleManagedDataClientDriver>();
                c.ConnectionString = connectionString;
                //// c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                //c.SchemaAction = SchemaAutoAction.Validate;
                c.LogFormattedSql = true;
                c.LogSqlInConsole = true;
            });
            //configuration.AddMapping(domainMapping);
            configuration.AddInputStream(NHibernate.Mapping.Attributes.HbmSerializer.Default.Serialize(System.Reflection.Assembly.GetExecutingAssembly()));



            var sessionFactory = configuration.BuildSessionFactory();

            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => {
                var interceptor = new SqlDebugOutputInterceptor();
                var session = sessionFactory.WithOptions()
                                            .Interceptor(interceptor)
                                            .OpenSession();
                return session;

            });
            //services.AddScoped<IMapperSession, NHibernateMapperSession>();

            return services;
        }
    }
}
