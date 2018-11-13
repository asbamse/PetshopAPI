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
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // GET api/address
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Address>> Get()
        {
            try
            {
                return _addressService.GetAll();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/address/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Address> Get(int id)
        {
            try
            {
                return _addressService.GetById(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/address
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Address> Post([FromBody] Address value)
        {
            try
            {
                return _addressService.Add(value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/address/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Address> Put(int id, [FromBody] Address value)
        {
            try
            {
                return _addressService.Update(id, value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/address/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Address> Delete(int id)
        {
            try
            {
                return _addressService.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
