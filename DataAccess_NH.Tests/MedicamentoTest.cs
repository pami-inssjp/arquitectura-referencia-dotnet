using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pami.DotNet.ReferenceArchitecture.DataAccess_NH.Tests;
using Pami.DotNet.ReferenceArchitecture.Modelo;
using NHibernate;
using System.Collections.Generic;


namespace DataAccess_NH.Tests
{
    [TestClass]
    public class MedicamentoTest : BaseEntityTest
    {
        [TestMethod]
        public void MedicamentoCreacion_Test()
        {
            Medicamento m = new Medicamento()
                        {
                            NombreComercial = "Amoxidal 500",
                            Monodroga = "Amoxicilina",
                            precioVenta = 50.25,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        };

            ISessionFactory sf = SessionFactoryProvider.GetSessionFactory();
            ISession session = sf.OpenSession();

            session.Transaction.Begin();

            session.SaveOrUpdate(m);

            session.Transaction.Commit();

            IList<Medicamento> meds = session.QueryOver<Medicamento>().List();

            Assert.AreEqual(1, meds.Count);

            Medicamento found = meds.FirstOrDefault();

            Assert.IsNotNull(found);
            Assert.AreEqual("Amoxidal 500", found.NombreComercial);
        }
    }
}
