using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bamz.Petshop.Core.ApplicationService;
using Bamz.Petshop.Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bamz.Petshop.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetTypesController : ControllerBase
    {
        private readonly IPetTypeService _petTypeService;

        public PetTypesController(IPetTypeService petTypeService)
        {
            _petTypeService = petTypeService;
        }

        // GET api/petType
        [Authorize]
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
        [Authorize]
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
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<PetType> Post([FromBody] PetType value)
        {
            try
            {
                return _petTypeService.Add(value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/petType/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<PetType> Put(int id, [FromBody] PetType value)
        {
            try
            {
                return _petTypeService.Update(id, value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/petType/5
        [Authorize(Roles = "Administrator")]
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
