using NHibernate.Cfg;
using NHibernate.Driver;
using NHibernate.Dialect;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;

namespace WirtualnyDziennik.Models
{
    public class NhibernateSession
    {
        public static ISession OpenSession()
        {
            String connectionString = "User ID=******;Password=*****;Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 212.33.90.213)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = XE)))";
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(OracleClientConfiguration.Oracle10.ConnectionString(connectionString)
                  .ConnectionString(connectionString)
                              .ShowSql()).Mappings(m => {
                   
                   m.FluentMappings.AddFromAssemblyOf<AutorTresci>();
                   m.FluentMappings.AddFromAssemblyOf<DziennikUcznia>();
                   m.FluentMappings.AddFromAssemblyOf<KlasaUczen>();
                   m.FluentMappings.AddFromAssemblyOf<Klasy>();
                   m.FluentMappings.AddFromAssemblyOf<PlanLekcji>();
                   m.FluentMappings.AddFromAssemblyOf<Przedmioty>();
                   m.FluentMappings.AddFromAssemblyOf<RodzicUczen>();
                   m.FluentMappings.AddFromAssemblyOf<Tresc>();
                   m.FluentMappings.AddFromAssemblyOf<TypTresci>();
                   m.FluentMappings.AddFromAssemblyOf<TypUzytkownika>();
                   m.FluentMappings.AddFromAssemblyOf<Uzytkownicy>();
                                  /*Tu dodawać kolejne tabele*/
                              })
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                                .Create(false, false))
                .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }

    /*    private static ISessionFactory CreateSessionFactory()
        {

            var c = Fluently.Configure();
            try
            {
                //Replace connectionstring and default schema
                c.Database(OracleDataClientConfiguration.Oracle10.
                    ConnectionString(x =>
                    x.FromConnectionStringWithKey("default"))
                   .DefaultSchema("SchemaName"));
                c.Mappings(m => m.FluentMappings.AddFromAssemblyOf<TypTresci>()
                c.Mappings(m => m.FluentMappings.AddFromAssemblyOf<Tresc>());
            }
            catch (Exception ex)
            {
                throw;
            }
            return c.BuildSessionFactory();
        }*/


    }
}