using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using Pami.DotNet.ReferenceArchitecture.Modelo;
using Pami.DotNet.ReferenceArchitecture.DataAccess.Base;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Dialect;
using NHibernate.Dialect.Schema;

namespace Pami.DotNet.ReferenceArchitecture.DataAccess_NH.Tests
{
    [TestClass]
    public class BaseEntityTest
    {
        public static ISessionFactoryProvider SessionFactoryProvider;

        /// <summary>
        /// Poor's man schema validation. 
        /// </summary>
        [TestMethod]
        public void TestDBCreation()
        {
            IConfiguracionProvider cfgProvider = ((TestSessionFactoryProvider)SessionFactoryProvider).GetConfiguracionProvider();
            Dialect dialect = Dialect.GetDialect(cfgProvider.Obtener().Properties);

            IDictionary<string, string> props = new Dictionary<string, string>(dialect.DefaultProperties);
            foreach (var prop in cfgProvider.Obtener().Properties)
            {
                props[prop.Key] = prop.Value;
            }
            var connectionHelper = new ManagedProviderConnectionHelper(props);
            connectionHelper.Prepare();
            
            DatabaseMetadata meta = new DatabaseMetadata(connectionHelper.Connection, dialect, false);
            ITableMetadata tableInfo =  meta.GetTableMetadata("MEDICAMENTO", null, null, true);
            Assert.IsNotNull(tableInfo);
        }

        
    }
}

