using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bamz.Petshop.Core.ApplicationService;
using Bamz.Petshop.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Bamz.Petshop.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetTypeController : ControllerBase
    {
        private readonly IPetTypeService _petTypeService;

        public PetTypeController(IPetTypeService petTypeService)
        {
            _petTypeService = petTypeService;
        }

        // GET api/petType
        [HttpGet]
        public ActionResult<IEnumerable<PetType>> Get()
        {
            try
            {
                return _petTypeService.GetAll();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/petType/5
        [HttpGet("{id}")]
        public ActionResult<PetType> Get(int id)
        {
            try
            {
                return _petTypeService.GetById(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/petType
        [HttpPost]
        public ActionResult<PetType> Post([FromBody] PetType value)
        {
            try
            {
                return _petTypeService.Add(value.Type);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/petType/5
        [HttpPut("{id}")]
        public ActionResult<PetType> Put(int id, [FromBody] PetType value)
        {
            try
            {
                return _petTypeService.Update(id, value.Type);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/petType/5
        [HttpDelete("{id}")]
        public ActionResult<PetType> Delete(int id)
        {
            try
            {
                return _petTypeService.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
