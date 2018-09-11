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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET api/order
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
        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order value)
        {
            try
            {
                return _orderService.Add(value.Customer, value.Pets, value.OrderDate, value.Price);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/order/5
        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id, [FromBody] Order value)
        {
            try
            {
                return _orderService.Update(id, value.Customer, value.Pets, value.OrderDate, value.Price);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/order/5
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
