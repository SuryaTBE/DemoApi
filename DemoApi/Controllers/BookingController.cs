using DemoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly DemoContext _db;
        public BookingController(DemoContext db)
        {
            _db= db;
        }
        [HttpPost]
        public async Task<ActionResult<BookingTbl>> Create(BookingTbl booking)
        {
            _db.BookingTbl.Add(booking);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<BookingTbl>> Index()
        {
            List<BookingTbl> book = new List<BookingTbl>();
            book=_db.BookingTbl.ToList();
            return Ok(book);
        }
        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<BookingTbl>> Details(int id)
        {
            var book = (from i in _db.BookingTbl
                        where i.BookingId == id
                        select i).FirstOrDefault();
            return Ok(book);
        }
        [HttpDelete]
        public async Task<ActionResult<BookingTbl>> Delete(int id)
        {
            var book = await _db.BookingTbl.FindAsync(id);
            _db.BookingTbl.Remove(book);
            _db.SaveChanges();
            return Ok();
        }
        [HttpPost]
        [Route("ProceedToPay")]
        public async Task<ActionResult<OrderMasterTbl>> ProceedToPay(OrderMasterTbl om)
        {
            _db.OrderMasterTbls.Add(om);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [Route("OrderMasterGetById")]
        public async Task<ActionResult<OrderMasterTbl>> OrderMasterGetById(int id)
        {
            var om=_db.OrderMasterTbls.Find(id);
            return Ok(om);
        }
        [HttpPut]
        public async Task<ActionResult<OrderMasterTbl>> OrderMasterPut(OrderMasterTbl om)
        {
            _db.OrderMasterTbls.Update(om);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        [Route("AddRangeOrderDetails")]
        public async Task<ActionResult<OrderDetailTbl>> AddRangeOrderDetails(List<OrderDetailTbl> od)
        {
            _db.OrderDetails.AddRange(od);
            await _db.SaveChangesAsync();
            return Ok();    
        }
        [HttpPost]
        [Route("RemoveRangeCart")]
        public async Task<ActionResult<BookingTbl>> RemoveRangeCart(List<BookingTbl> bt)
        {
            _db.BookingTbl.RemoveRange(bt);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        [Route("ListConvert")]
        public async Task<ActionResult<BookingTbl>> ListConvert(BookingTbl booking)
        {
            List<BookingTbl> book = new List<BookingTbl>();
            book=(from i in _db.BookingTbl
                  where i.UserId==booking.UserId select i).ToList();
            return Ok(book);
        }
    }
}
