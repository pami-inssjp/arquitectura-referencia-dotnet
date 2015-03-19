using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pami.DotNet.ReferenceArchitecture.Modelo
{
    public class Medicamento : BaseEntity
    {
        public virtual string NombreComercial { get; set; }

        public virtual string Monodroga { get; set; }

        public virtual double precioVenta { get; set; }

    }
}
