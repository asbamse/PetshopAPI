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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET api/order
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            try
            {
                return _orderService.GetAll();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/order/5
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            try
            {
                return _orderService.GetById(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/order
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order value)
        {
            try
            {
                return _orderService.Add(value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/order/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id, [FromBody] Order value)
        {
            try
            {
                return _orderService.Update(id, value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/order/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Order> Delete(int id)
        {
            try
            {
                return _orderService.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
