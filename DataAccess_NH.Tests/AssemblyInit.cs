using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pami.DotNet.ReferenceArchitecture.DataAccess.Base;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using NHibernate;
using NHibernate.Dialect;
using System.Collections.Generic;
using NHibernate.Util;
using NHibernate.Dialect.Schema;
using NHibernate.Mapping;
using System.Data;

namespace Pami.DotNet.ReferenceArchitecture.DataAccess_NH.Tests
{
    [TestClass]
    public class AssemblyInit
    {
        private const string connectionString = "Data Source=:memory:;Version=3;New=True;";


        public AssemblyInit()
        {
            //
            // TODO: Add constructor logic here
            //
        }



        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            IConfiguracionProvider configuracionProvider = new ConfiguracionProvider(cfg =>
            {
                var schemaExport = new SchemaExport(cfg);
                schemaExport.Execute(true, true, false);
            });
            BaseEntityTest.SessionFactoryProvider = new TestSessionFactoryProvider(configuracionProvider);
            
        }
    }



    /// <summary>
    /// Clase que implementa la interfaz ISessionFactoryProvider a diferencia de 
    /// SessionFactoryProvider la instancia de ISessionFactory es estática 
    /// y la inicialización se realiza en el método constructor.
    /// </summary>
    public class TestSessionFactoryProvider : ISessionFactoryProvider
    {
        private static ISessionFactory sessionFactory;
        private IConfiguracionProvider cfgProvider;

        /// <summary>
        /// Constructor de clase, recibe una instancia de IConfiguracionProvider
        /// con la configuración de NHibernate que se quiera utilizar para la ejecución 
        /// de los tests.
        /// </summary>
        /// <param name="configProvider"></param>
        public TestSessionFactoryProvider(IConfiguracionProvider configProvider)
        {
            if (configProvider == null)
                throw new ArgumentNullException("configuracionProvider");

            this.cfgProvider = configProvider;
            InitializeSessionFactory();
        }

        private void InitializeSessionFactory()
        {
            if (sessionFactory == null)
            {
                //log.Debug("Initialize a new session factory reading the configuration.");
                Configuration conf = this.cfgProvider.Obtener();
                if (conf != null)
                {
                    sessionFactory = conf.BuildSessionFactory();
                }
            }
        }

        public IConfiguracionProvider GetConfiguracionProvider()
        {
            return cfgProvider;
        }

        public ISessionFactory GetSessionFactory()
        {
            return sessionFactory;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
