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

        //[HttpPost]
        //public async Task<ActionResult<BookingTbl>> Create(BookingTbl booking)
        //{
        //    _db.BookingTbl.Add(booking);
        //    await _db.SaveChangesAsync();
        //    return Ok();
        //}
        [HttpGet]
        public async Task<ActionResult<BookingTbl>> Index()
        {
            List<BookingTbl> cart = await _db.BookingTbl.ToListAsync();
            return Ok(cart);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingTbl>> Details(int id)
        {
            var book = (from i in _db.BookingTbl
                        where i.BookingId == id
                        select i).FirstOrDefault();
            return Ok(book);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookingTbl>> Delete(int id)
        {
            var book = await _db.BookingTbl.FindAsync(id);
            _db.BookingTbl.Remove(book);
            _db.SaveChanges();
            return Ok();
        }
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<BookingTbl>> Create(BookingTbl booking)
        {
            int i = SeatCheck(booking.SeatNo, booking.Date, booking.MovieId);
            if (i == 1)
            {
                _db.Add(booking);
                await _db.SaveChangesAsync();
            }
            else
            {
                booking.SeatNo = "Null";
                await _db.SaveChangesAsync();

            }
            return Ok(booking);
        }
        [HttpPost]
        [Route("ProceedtoBuy")]
        public async Task<OrderMasterTbl> ProceedtoBuy(int id)
        {
            //var UserId = HttpContext.Session.GetInt32("Userid");
            List<BookingTbl> cart = (from i in _db.BookingTbl where i.UserId == id select i).ToList();
            OrderMasterTbl om = new OrderMasterTbl();
            foreach (var i in cart)
            {
                int a = SeatCheck(i.SeatNo, i.Date, i.MovieId);
                if (a == 0)
                {
                    om.Amount = 1;
                }
            }
            if (om.Amount != 1)
            {
                om.OrderDate = DateTime.Today;
                om.UserId = (int)id;
                om.Amount = 0;
                foreach (var item in cart)
                {

                    om.Amount += (int)item.AmountTotal;
                }
                _db.Add(om);

                _db.SaveChanges();
                return om;
            }
            else
                return om;   
        }
        [HttpGet]
        [Route("GetPaymentById")]
        public async Task<OrderMasterTbl> GetPaymentById(int id)
        {
            OrderMasterTbl result = await _db.OrderMasterTbls.FindAsync(id);
            return result;

            //var OrderMaster = _context.OrderMasters.Find(id);
            //return View(OrderMaster);
        }
        [HttpPost]
        [Route("Payment")]
        public async Task<IActionResult> Payment(OrderMasterTbl m)
        {
            List<BookingTbl> cart = (from i in _db.BookingTbl where i.UserId == m.UserId select i).ToList();
            List<OrderDetailTbl> od = new List<OrderDetailTbl>();
            if (m.Paid == m.Amount)
            {
                List<BookingTbl> book = (from i in _db.BookingTbl where i.UserId == m.UserId select i).ToList();
                _db.OrderMasterTbls.Update(m);
                _db.SaveChanges();//ordermaster update in api
                foreach (var j in book)
                {
                    var s = (from i in _db.MovieTbls
                             where i.MovieId == j.MovieId
                             select i).SingleOrDefault();
                    s.capacity -= j.NoOfTickets;
                    _db.MovieTbls.Update(s);//update in movie controller api
                }
                _db.SaveChanges();
                foreach (var item in cart)
                {
                    OrderDetailTbl detail = new OrderDetailTbl();
                    detail.MovieId = item.MovieId;
                    detail.NoOfTickets = (int)item.NoOfTickets;
                    detail.MovieName = item.MovieName;
                    detail.UserId = m.UserId;
                    detail.MovieDate = item.Date;
                    detail.Slot =item.Slot;
                    detail.SeatNo = item.SeatNo;
                    detail.Cost = item.AmountTotal;
                    detail.OrderMasterId = m.OrderMasterId;
                    od.Add(detail);
                }
                _db.AddRange(od);
                _db.SaveChanges();//addrangeorderdetails in api
                _db.BookingTbl.RemoveRange(book);
                _db.SaveChanges();
                return Ok(m);
            }
            else
            {
                OrderMasterTbl om = new OrderMasterTbl();
                om.Paid = 0;
                return Ok(om);
            }
        }
        [HttpGet]
        [Route("Seatcheck")]
        public int SeatCheck(string a, DateTime d, int id)//Seat availability
        {
            List<OrderDetailTbl> detail = _db.OrderDetails.ToList();
            string[] SeatList = a.Split(",", StringSplitOptions.RemoveEmptyEntries);
            for (int b = 0; b < SeatList.Length; b++)
            {
                foreach (var od in detail)//check in orderdetails
                {
                    string[] Seatnos = od.SeatNo.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < Seatnos.Length; j++)
                    {
                        if ((od.MovieDate == d) && (od.MovieId == id) && (Seatnos[j] == SeatList[b]))
                        {
                            return 0;
                        }
                    }
                }
            }
            return 1;
        }

    }
}
