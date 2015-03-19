using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Pami.DotNet.ReferenceArchitecture.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pami.DotNet.ReferenceArchitecture.DataAccess.Mappings
{
    public class BaseEntityMap : ClassMapping<BaseEntity>
    {
        public BaseEntityMap()
        {
           Id(e => e.Id, m => m.Generator(Generators.Identity));
           Version(e => e.Version, m =>
            {
                m.Generated(VersionGeneration.Never);
                m.UnsavedValue(0);
                m.Insert(true);
            });
           Property(e => e.CreatedDate, m => m.Column("CreatedDate"));
           Property(e => e.UpdatedDate, m => m.Column("UpdatedDate"));
        }
    }
}
