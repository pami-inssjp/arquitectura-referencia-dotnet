using System;
using System.Collections;
using System.Collections.Generic;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.Utils
{
    /// <summary>
    /// Contexto de un thread, en el que se pueden almacenar objetos para el uso dentro dedicho thread.
    /// Es responsabilidad de quien lo use eliminar el objeto una vez que se haya dejado de usar y antes que el thread finalice.
    /// </summary>
    /// <remarks>Se utiliza en reemplazo del <code>HttpContext</code> en aquellos escenarios donde no se dispone del mismo.</remarks>
    public class ThreadContext : IDictionary
    {
        #region Variable de Instancia

        [ThreadStatic]
        private static IDictionary datos;

        #endregion

        #region Propiedades

        /// <summary>
        /// Estructura de datos que mantiene la información referente al thread.
        /// </summary>
        public IDictionary Items
        {
            get
            {
                if (datos == null)
                    datos = new Hashtable();
                return datos;
            }
        }

        /// <summary>
        /// Devuelve la instancia actual de contexto del thread.
        /// </summary>
        public static ThreadContext Current
        {
            get 
            {
                return new ThreadContext();
            }
        }

        #endregion

        #region IDictionary

        public void Add(object key, object value)
        {
            this.Items.Add(key, value);
        }

        public void Clear()
        {
            this.Items.Clear();
        }

        public bool Contains(object key)
        {
            return this.Items.Contains(key);
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        public bool IsFixedSize
        {
            get { return this.Items.IsFixedSize; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public ICollection Keys
        {
            get { return this.Items.Keys; }
        }

        public void Remove(object key)
        {
            this.Items.Remove(key);
        }

        public ICollection Values
        {
            get { return this.Items.Values; }
        }

        public object this[object key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException("clave");
                if (!this.Items.Contains(key))
                    throw new KeyNotFoundException(string.Format("No se encontró el item con clave {0} en el contexto del thread.", key));

                return this.Items[key];
            }
            set
            {
                this.Items[key] = value;
            }
        }

        public void CopyTo(Array array, int index)
        {
            this.Items.CopyTo(array, index);
        }

        public int Count
        {
            get { return this.Items.Count; }
        }

        public bool IsSynchronized
        {
            get { return this.Items.IsSynchronized; }
        }

        public object SyncRoot
        {
            get { return this.Items.SyncRoot; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        #endregion
    }
}
