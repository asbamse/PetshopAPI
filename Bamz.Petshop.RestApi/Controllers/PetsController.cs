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
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        // GET api/pet
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get()
        {
            try
            {
                return _petService.GetAll();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/pet
        [Authorize]
        [HttpGet("filtered/{filter}")]
        public ActionResult<IEnumerable<Pet>> Get([FromQuery]PageProperty pageProperty)
        {
            try
            {
                return _petService.GetPage(pageProperty);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/pet/5
        [Authorize]
        [HttpGet("id/{id}")]
        public ActionResult<Pet> Get(int id)
        {
            try
            {
                return _petService.GetById(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/pet
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet value)
        {
            try
            {
                return _petService.Add(value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/pet/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, [FromBody] Pet value)
        {
            try
            {
                value.Id = id;
                return _petService.Update(id, value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/pet/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            try
            {
                return _petService.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
