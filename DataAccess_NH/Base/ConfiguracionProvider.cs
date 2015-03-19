using System;
using System.IO;
using System.Linq;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Engine;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Type;
using Fhwk.Core;
using Pami.DotNet.ReferenceArchitecture.Modelo;
using System.Reflection;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.Data;

namespace Pami.DotNet.ReferenceArchitecture.DataAccess.Base
{

    #region Interfaz 

    /// <summary>
    /// Provider de la configuración de NHibernate
    /// </summary>
    public interface IConfiguracionProvider
    {
        /// <summary>
        /// Devuelve la instancia de la configuración de NHibernate
        /// </summary>
        /// <returns></returns>
        Configuration Obtener();
    }

    #endregion

    #region Implementacion por Defecto

    /// <summary>
    /// <para>
    /// Implementación del Provider de la configuración de NHibernate que utiliza la siguiente política:
    /// </para>
    /// <list type="number">
    ///     <item>
    ///         <description>
    ///         Si existe un archivo de configuración de NHibernate (<code>hibernate.cfg.xml</code>) o
    ///         una sección en el <code>.config</code> correspondiente, toma dicha configuración.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         Luego setea la configuración adicional: Valida el esquema al iniciar.
    ///         Además, en DEBUG (Desarrollo), loguea las SQLs y genera estadísticas.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public class ConfiguracionProvider : IConfiguracionProvider
    {

        #region Constantes
        
        /// <summary>
        /// Nombre del archivo de configuración de NHibernate
        /// </summary>
        protected const string ConfigurationFileName = "hibernate.cfg.xml";
        
        /// <summary>
        /// Nombres clásicos de la sección de configuración de NHibernate
        /// </summary>
        protected string[] ConfigurationSectionNames = { "hibernate", "hibernate-configuration" };

        #endregion

        #region Variables de instancia

        /// <summary>
        /// Configuración de NH usada.
        /// </summary>
        protected Configuration configuration;

        /// <summary>
        /// Objeto utilizado para bloquear la creación de la configuración mientras otro thread la está creando.
        /// </summary>
        private object creando = new object();

        /// <summary>
        /// Accion sobre la configuración a ejecutar ni bien esté disponible
        /// </summary>
        Action<Configuration> postCrear;

        #endregion

        #region Constructor

        public ConfiguracionProvider()
        {
        }

        public ConfiguracionProvider(Action<Configuration> postCrear)
        {
            this.postCrear = postCrear;
        }

        #endregion

        #region IConfiguracionProvider

        /// <summary>
        /// Devuelve la instancia de la configuración de NHibernate
        /// </summary>
        /// <returns>Configuración de NHibernate</returns>
        public virtual Configuration Obtener()
        {
            if (this.configuration == null)
            {
                lock(creando)
                {
                    this.configuration = Crear();
                }
            }
            return this.configuration;
        }

        #endregion

        #region Métodos Auxiliares

        /// <summary>
        /// Crea la configuración de NHibernate
        /// </summary>
        /// <returns>Instancia de la configuración</returns>
        protected virtual Configuration Crear()
        {
            Configuration cfg = new Configuration();

            CargarDeArchivo(cfg);

            ConfigurarDB(cfg);

            CargarMappings(cfg);

            ConfigurarSessionContext(cfg);

            SchemaMetadataUpdater.QuoteTableAndColumns(cfg);

            if (this.postCrear != null)
                this.postCrear.Invoke(cfg);
            
            return cfg;
        }


        /// <summary>
        /// Carga el archivo/sección de configuración de Nhibernate si existe.
        /// </summary>
        /// <param name="cfg">Configuración de NHiberante</param>
        protected virtual void CargarDeArchivo(Configuration cfg)
        {
            if (ExisteArchivoConfiguracion() || ExisteSeccionConfiguracion())
                cfg.Configure();
        }


        /// <summary>
        /// Realiza la configuración de la NHibernate en forma programática con valores por defecto.
        /// Deberá existir un elemento connectionString en el archivo de configuración de la app (App.config Web.config), 
        /// llamado PamiRef con el connection string para la base de datos.
        ///     <connectionStrings>
        ///         <add name="PamiRef" connectionString="Server=.\SQLExpress;AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\PAMI_REF.mdf;Database=PAMI_REF;Trusted_Connection=Yes;"
        ///             providerName="System.Data.SqlClient" />
        ///     </connectionStrings>
        /// </summary>
        /// <param name="cfg">Configuración de NHiberante</param>
        protected virtual void ConfigurarDB(Configuration cfg)
        {
            var ConnectionStringName = "PamiRef";
            cfg.DataBaseIntegration(db =>
                  {
                    db.Dialect<MsSql2012Dialect>();
                    db.Driver<SqlClientDriver>();
                    db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                    db.IsolationLevel = IsolationLevel.ReadCommitted;
                    //db.SchemaAction = SchemaAutoAction.Validate;
                    db.ConnectionStringName = ConnectionStringName;
                  });
        }

        /// <summary>
        /// Determina si existe el archivo de configuración de NHibernate
        /// </summary>
        /// <returns><code>True</code> si existe el archivo; <code>False</code> en caso contrario</returns>
        protected virtual bool ExisteArchivoConfiguracion()
        {
            string fileBasePath = AppDomain.CurrentDomain.BaseDirectory;
            if (Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin")))
                fileBasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

            return File.Exists(Path.Combine(fileBasePath, ConfigurationFileName));
        }

        /// <summary>
        /// Determina si existe la sección de configuración de NHibernate.
        /// </summary>
        /// <returns><code>True</code> si existe la sección; <code>False</code> en caso contrario</returns>
        protected virtual bool ExisteSeccionConfiguracion()
        {
            return ConfigurationSectionNames.Any(n => 
            { 
                try
                {
                    return System.Configuration.ConfigurationManager.GetSection(n) != null; 
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        /// <summary>
        /// Carga la información de mapeo de entidades en la configuración.
        /// </summary>
        /// <param name="cfg"></param>
        protected virtual void CargarMappings(Configuration cfg)
        {
            Firehawk.Init()
                .Configure()
                    .ConfigureEntities()
                        .AddBaseEntity<BaseEntity>()
                        .SearchForEntitiesOnTheseAssemblies(a => a.FullName.StartsWith("Pami.DotNet.ReferenceArchitecture.Modelo"))
                        .EndConfig()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForTableNames(TablesNamingConvention.Uppercase)
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameRelationshipName)
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableNameSecondTableName)
                        .UseConventionForColumnNames(ColumnsNamingConvention.Lowercase)
                        .UseConventionForPrimaryKeyColumnNames(PrimaryKeyColumnNamingConvention.EntityNameIdPropertyName)
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyNameIdPropertyName)
                        .UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.EntityPropertyName_ComponentPropertyName)
                        .EndConfig()
                    .EndConfiguration()
                    .BuildMappings(cfg);
        }

        /// <summary>
        /// Configura la implementacion de session context a utilizar
        /// </summary>
        /// <param name="cfg">Configuracion de NH</param>
        protected virtual void ConfigurarSessionContext(Configuration cfg)
        {
            cfg.CurrentSessionContext<LazySessionContext>();
        }

        #endregion
    }

    #endregion

}
