using DemoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly DemoContext _db;
        public OrderDetailsController(DemoContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<ActionResult<OrderDetailTbl>> Index()
        {
            return Ok(_db.OrderDetails.ToList());
        }
        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<OrderDetailTbl>> Details(int id)
        {
            var detail=(from i in _db.OrderDetails
                         where i.OrderDetailId==id
                         select i).FirstOrDefault();
            return Ok(detail);
        }
        [HttpPost]
        public async Task<ActionResult<OrderDetailTbl>> Cancel(OrderDetailTbl od)
        {
            _db.OrderDetails.Remove(od);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
