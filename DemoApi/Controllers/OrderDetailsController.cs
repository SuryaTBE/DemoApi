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
            List<OrderDetailTbl> od = new List<OrderDetailTbl>();
            od =await _db.OrderDetails.ToListAsync();
            return Ok(od);
        }
        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<OrderDetailTbl>> Details(int id)
        {
            var detail=await _db.OrderDetails.FindAsync(id);
            return Ok(detail);
        }
        [HttpPost]
        public async Task<ActionResult<OrderDetailTbl>> Cancel(int id)
        {
            var orderdetail = await _db.OrderDetails.FindAsync(id);
            int no = orderdetail.NoOfTickets;
            int mid = (int)orderdetail.MovieId;
            var s = (from i in _db.MovieTbls
                     where i.MovieId == mid
                     select i).SingleOrDefault();
            s.capacity += no;
            _db.OrderDetails.Remove(orderdetail);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
