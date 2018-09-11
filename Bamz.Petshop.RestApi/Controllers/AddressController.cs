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
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // GET api/address
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
        [HttpPost]
        public ActionResult<Address> Post([FromBody] Address value)
        {
            try
            {
                return _addressService.Add(value.Street, value.Number, value.Letter, value.Floor, value.Side, value.ZipCode, value.City);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/address/5
        [HttpPut("{id}")]
        public ActionResult<Address> Put(int id, [FromBody] Address value)
        {
            try
            {
                return _addressService.Update(id, value.Street, value.Number, value.Letter, value.Floor, value.Side, value.ZipCode, value.City);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/address/5
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
