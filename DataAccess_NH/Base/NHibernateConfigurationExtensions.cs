using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Mapping;

namespace Pami.DotNet.ReferenceArchitecture.DataAccess.Base
{
    /// <summary>
    /// Extensión sobre <code>Configuration</code> de NHibernate para generar los índices correspondientes a las
    /// Foreing Keys.
    /// </summary>
    public static class NHibernateConfigurationExtensions
    {
        private static readonly PropertyInfo TableMappingsProperty =
            typeof(Configuration).GetProperty("TableMappings", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        /// <summary>
        /// Configura la creación de índices para las foreing keys.
        /// </summary>
        /// <param name="configuration">Objecto de configuración de NHibernate</param>
        public static void CreateIndexesForForeignKeys(this Configuration configuration)
        {
            configuration.BuildMappings();
            var tables = (ICollection<Table>)TableMappingsProperty.GetValue(configuration, null);
            foreach (var table in tables)
            {
                foreach (var foreignKey in table.ForeignKeyIterator)
                {
                    var idx = new Index();
                    idx.AddColumns(foreignKey.ColumnIterator);
                    idx.Name = "IX_" + foreignKey.Name.Substring(3);
                    idx.Table = table;
                    if (!table.IndexIterator.Any(ix => ix.Name == idx.Name))
                        table.AddIndex(idx);
                }
            }
        }
    }
}
