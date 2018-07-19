using System.Collections.Generic;
using Main.Models;
using Main.Service;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedureController : ControllerBase
    {
        private IRepository _repository;

        public ProcedureController(IRepository repository)
        {
            _repository = repository;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Table>> Get()
        {
            var result = _repository.Get();
            return Ok(result);
        }
    }
}