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
    public class ColoursController : ControllerBase
    {
        private readonly IColourService _colourService;

        public ColoursController(IColourService colourService)
        {
            _colourService = colourService;
        }

        // GET api/colour
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Colour>> Get()
        {
            try
            {
                return _colourService.GetAll();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/colour/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Colour> Get(int id)
        {
            try
            {
                return _colourService.GetById(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/colour
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Colour> Post([FromBody] Colour value)
        {
            try
            {
                return _colourService.Add(value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/colour/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Colour> Put(int id, [FromBody] Colour value)
        {
            try
            {
                return _colourService.Update(id, value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/colour/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Colour> Delete(int id)
        {
            try
            {
                return _colourService.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
