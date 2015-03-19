using Pami.DotNet.ReferenceArchitecture.Modelo;
using Pami.DotNet.ReferenceArchitecture.Modelo.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Pami.DotNet.ReferenceArchitecture.Servicios.Controllers
{
    [Authorize]
    public class MedicamentoController : ApiController
    {
        private IMedicamentoRepo repo;

        public MedicamentoController(IMedicamentoRepo repoMedicamento)
        {
            repo = repoMedicamento;
        }



        // GET api/<controller>
        public IEnumerable<Medicamento> Get()
        {
            return repo.ObtenerTodos();
        }

        // GET api/<controller>/5
        public Medicamento Get(int id)
        {
            return repo.Obtener(id);
        }

        // POST api/<controller>
        public void Post([FromBody] Medicamento value)
        {
            repo.Agregar(value);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpGet]
        //[Route("api/medicamento/bymonodroga/{monodroga}")]
        public IEnumerable<Medicamento> ByMonodroga(string id)
        {
            return repo.BuscarPorMonodroga(id);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}