using DemoApi.Controllers;
using DemoApi.Models;
using DemoApi.Tests.InMemoryDatabase;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Tests.Controllers
{
    public class BookingControllerTests
    {
        [Fact]
        public async Task IndexTests()
        {
            //Arrange
            var inmemory = new DemoInMemoryDatabase();
            var dbcontext = await inmemory.GetDatabaseContext();
            var booking = new BookingController(dbcontext);

            //Act
            var result=await booking.Index();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<BookingTbl>>();
            dbcontext.BookingTbl.Should().HaveCount(2);
        }
        [Fact]
        public async Task DetailsTests()
        {
            //Arrange
            var inmemory=new DemoInMemoryDatabase();
            var dbcontext=await inmemory.GetDatabaseContext();
            var booking=new BookingController(dbcontext);
            BookingTbl b = new BookingTbl()
            {
                BookingId = 401
            };

            //Act
            var result = await booking.Details(b.BookingId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<BookingTbl>>();
        }
        [Fact]
        public async Task DeleteTests()
        {
            //Arrange
            var inmemory=new DemoInMemoryDatabase();
            var dbcontext= await inmemory.GetDatabaseContext();
            var booking= new BookingController(dbcontext);
            int id = 402;

            //Act
            var result= await booking.Delete(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<BookingTbl>>();
            dbcontext.BookingTbl.Should().HaveCount(1);
        }
        [Fact]
        public async Task CreateTests()
        {
            //Arrange
            var inmemory=new DemoInMemoryDatabase();
            var dbcontext=await inmemory.GetDatabaseContext();
            var booking=new BookingController(dbcontext);
            BookingTbl b = new BookingTbl()
            {
                BookingId = 403,
                MovieId = 302,
                MovieName = "Avatar2",
                Date = new DateTime(2022, 12, 31),
                Slot = "9:30PM",
                UserId = 202,
                NoOfTickets = 2,
                SeatNo = "49,50",
                AmountTotal = 300
            };

            //Act
            var result= await booking.Create(b);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<BookingTbl>>();
            dbcontext.BookingTbl.Should().HaveCount(3);
        }
        [Fact]
        public async Task ProceedToBuyTests()
        {
            //Arrange
            var inmemory=new DemoInMemoryDatabase();
            var dbcontext=await inmemory.GetDatabaseContext();
            var booking=new BookingController(dbcontext);
            int Uid = 201;

            //Act
            var result = await booking.ProceedtoBuy(Uid);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OrderMasterTbl>();
            dbcontext.OrderMasterTbls.Should().HaveCount(3);
        }
        [Fact]
        public async Task GetPaymentByIdTests()
        {
            //Arrange
            var inmemory=new DemoInMemoryDatabase();
            var dbcontext=await inmemory.GetDatabaseContext();
            var booking = new BookingController(dbcontext);
            int omid = 501;

            //Act
            var result=await booking.GetPaymentById(omid);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OrderMasterTbl>();
            result.OrderMasterId.Should().Be(omid);
        }
        [Fact]
        public async Task PaymentTests()
        {
            //Arrange
            var inmemory=new DemoInMemoryDatabase();
            var dbcontext= await inmemory.GetDatabaseContext();
            var booking= new BookingController(dbcontext);
            OrderMasterTbl om = new OrderMasterTbl()
            {
                OrderMasterId = 502,
                UserId = 201,
                OrderDate = DateTime.Now,
                CardNo = 12345,
                Amount = 300,
                Paid = 300
            };

            //Act
            var m = await dbcontext.OrderMasterTbls.FindAsync(om.OrderMasterId); ;
            var result=await booking.Payment(m);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            dbcontext.OrderMasterTbls.Should().HaveCount(2);
            dbcontext.OrderDetails.Should().HaveCount(3);
            dbcontext.BookingTbl.Should().HaveCount(1);
        }
        [Fact]
        public async Task SeatCheckTests()
        {
            //Arrange
            var inmemory=new DemoInMemoryDatabase();
            var dbcontext=await inmemory.GetDatabaseContext(); 
            var booking=new BookingController(dbcontext);

            //Act
            var result =booking.SeatCheck("49,50", new DateTime(2022, 12, 31), 301);

            //Assert
            result.Should().NotBe(0);
            result.Should().Be(1);
            result.Should().BeGreaterThan(0);
            dbcontext.OrderDetails.Should().HaveCount(2);
        }

    }
}
