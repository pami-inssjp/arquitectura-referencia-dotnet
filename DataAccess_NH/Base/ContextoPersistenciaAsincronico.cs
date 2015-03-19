using System;

namespace Pami.DotNet.ReferenceArchitecture.DataAccess.Base
{
    /// <summary>
    /// Contexto de persistencia utilizado en threads que no se disparan a razon de threads HTTP, sino 
    /// que son generados internamente por la aplicación.
    /// </summary>
    public interface IContextoPersistenciaAsincronico : IDisposable
    {

        /// <summary>
        /// Inicializa el contexto de persistencia.
        /// </summary>
        IContextoPersistenciaAsincronico Iniciar();

        /// <summary>
        /// Libera el contexto de persistencia.
        /// </summary>
        void Cerrar();

    }


    /// <summary>
    /// Permite inicializar/finalizar el contexto de persistencia (manejo de sesión de NH) de manera manual. 
    /// Se utiliza principalmente para threads asincrónicos (no requests HTTP) que necesitan
    /// realizar accesos a la BD.
    /// </summary>
    public class ContextoPersistenciaAsincronico : ContextoPersistenciaBase, IContextoPersistenciaAsincronico
    {

        #region IContextoPersistenciaAsincronico

        /// <summary>
        /// Inicializa el contexto de persistencia, ligando un inicializador de la sesión de NH
        /// al <code>SessionContext</code> de NH.
        /// </summary>
        public IContextoPersistenciaAsincronico Iniciar()
        {
            IniciarContexto();
            return this;
        }

        /// <summary>
        /// Libera el contexto de persistencia, desligando la sessión del NH del thread.
        /// </summary>
        public void Cerrar()
        {
            FinalizarContexto();
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Libera el contexto de persistencia
        /// </summary>
        public void Dispose()
        {
            Cerrar();
        }

        #endregion

    }
}
