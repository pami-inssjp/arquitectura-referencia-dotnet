using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.TypeScanner
{
    /// <summary>
    /// Provee métodos auxiliares para cargar assemblies
    /// </summary>
    public class AssemblyHelper
    {
        #region Métodos públicos

        /// <summary>
        /// Retorna una colección de assemblies cuyo nombre cumpla con el prefijo indicado. 
        /// </summary>
        /// <param name="assemblyPrefix">Prefijo de los assemblies a escanear.</param>
        public static IEnumerable<Assembly> GetAssemblies(string assemblyPrefix)
        {
            IEnumerable<Assembly> assemblies;
            if (string.IsNullOrWhiteSpace(assemblyPrefix))
                assemblies = AppDomain.CurrentDomain.GetAssemblies().AsEnumerable();
            else
                assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where<Assembly>(a => a.FullName.StartsWith(assemblyPrefix));
            return assemblies;
        }

        /// <summary>
        /// Chequea que todos los assemblies de la aplicación (que respetan el prefijo indicado) hayan sido cargados
        /// por el runtime subyacente. Aquellos que no fueron cargados, los carga individualmente. 
        /// </summary>
        /// <param name="assemblyPrefix">Prefijo de los assemblies a escanear.</param>
        public static void VerifyLoadedAssemblies(string assemblyPrefix)
        {
            // Obtengo los assemblies de la aplicación ya cargados
            Dictionary<string, Assembly> loadedAssemblies = GetLoadedAssemblies(assemblyPrefix);

            // Obtengo los nombres de los assemblies de la aplicación que están en el directorio bin
            string[] assembliesInFolder = GetAssembliesInFolder(assemblyPrefix);

            if (assembliesInFolder.Length != loadedAssemblies.Count)
            {
                LoadUnloadedAssemblies(assembliesInFolder, loadedAssemblies);
            }
        }

        #endregion

        #region Métodos auxiliares

        private static Dictionary<string, Assembly> GetLoadedAssemblies(string assemblyPrefix)
        {
            return AppDomain.CurrentDomain
                            .GetAssemblies()
                            .Where<Assembly>(a => a.FullName.StartsWith(assemblyPrefix)).ToDictionary<Assembly, string>(a => a.GetName().Name);
        }

        private static string[] GetAssembliesInFolder(string assemblyPrefix)
        {
            string baseFolder = GetAppAssembliesFolder();
            Trace.TraceInformation("Directorio base de los assemblies:: " + baseFolder);
            return Directory.GetFiles(baseFolder, assemblyPrefix + "*.dll");
        }

        private static string GetAppAssembliesFolder()
        {
            if (Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin")))
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            else
                return AppDomain.CurrentDomain.BaseDirectory;
        }

        private static void LoadUnloadedAssemblies(string[] assembliesInFolder, Dictionary<string, Assembly> loadedAssemblies)
        {
            Trace.TraceInformation("Cantidad de assemblies cargados:: " + loadedAssemblies.Count());
            Trace.TraceInformation("Cantidad de assemblies carpeta base:: " + assembliesInFolder.Length);

            foreach (var assemblyFilename in assembliesInFolder)
            {
                string assemblyName = Path.GetFileNameWithoutExtension(assemblyFilename);
                if (!loadedAssemblies.ContainsKey(assemblyName))
                {
                    Trace.TraceInformation("Cargando el assembly:: " + assemblyName);
                    Assembly.Load(assemblyName);
                    Trace.TraceInformation("Assembly Cargado:: " + assemblyName);
                }
            }
        }

        #endregion

    }
}
