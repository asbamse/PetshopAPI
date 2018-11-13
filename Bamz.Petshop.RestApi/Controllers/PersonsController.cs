using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bamz.Petshop.Core.ApplicationService;
using Bamz.Petshop.Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bamz.Petshop.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET: api/person
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            try
            {
                return _personService.GetAll();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/person/5
        [Authorize]
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Person> Get(int id)
        {
            try
            {
                return _personService.GetById(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/person
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Person> Post([FromBody] PersonInput value)
        {
            try
            {
                return _personService.Add(value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/person/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Person> Put(int id, [FromBody] PersonInput value)
        {
            try
            {
                return _personService.Update(id, value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/person/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Person> Delete(int id)
        {
            try
            {
                return _personService.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
