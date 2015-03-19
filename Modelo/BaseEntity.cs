using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pami.DotNet.ReferenceArchitecture.Modelo
{
    public class BaseEntity
    {
        /// <summary>
        /// El Id de la entidad
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the a number that identifies the entity version. This value is used with optimist lock feature.
        /// </summary>
        public virtual int Version { get; protected set; }

        /// <summary>
        /// Fecha de creación de la entidad
        /// </summary>
        public virtual DateTime CreatedDate { get; set; }

        /// <summary>
        /// Fecha de última actualización
        /// </summary>
        public virtual DateTime UpdatedDate { get; set; }
    }
}
