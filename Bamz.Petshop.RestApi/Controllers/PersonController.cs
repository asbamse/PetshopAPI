using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bamz.Petshop.Core.ApplicationService;
using Bamz.Petshop.Core.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bamz.Petshop.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET: api/person
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
        [HttpPost]
        public ActionResult<Person> Post([FromBody] Person value)
        {
            try
            {
                return _personService.Add(value.FirstName, value.LastName, value.Address, value.Phone, value.Email);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/person/5
        [HttpPut("{id}")]
        public ActionResult<Person> Put(int id, [FromBody] Person value)
        {
            try
            {
                return _personService.Update(id, value.FirstName, value.LastName, value.Address, value.Phone, value.Email);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/person/5
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
