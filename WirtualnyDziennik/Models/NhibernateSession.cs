using NHibernate.Cfg;
using NHibernate.Driver;
using NHibernate.Dialect;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;

namespace WirtualnyDziennik.Models
{
    public class NhibernateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            var configurationPath = HttpContext.Current.Server.MapPath(@"~\Models\hibernate.cfg.xml");
            configuration.Configure(configurationPath);
            var typConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\TypTresci.hbm.xml");
            configuration.AddFile(typConfigurationFile);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}