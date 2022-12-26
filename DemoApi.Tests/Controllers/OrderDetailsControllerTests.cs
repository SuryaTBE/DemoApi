using DemoApi.Controllers;
using DemoApi.Models;
using DemoApi.Tests.InMemoryDatabase;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Tests.Controllers
{
    public class OrderDetailsControllerTests
    {
        [Fact]
        public async Task IndexTests()
        {
            //Arrange
            var inmemory = new DemoInMemoryDatabase();
            var dbcontext=await inmemory.GetDatabaseContext();
            var orderdetails = new OrderDetailsController(dbcontext);

            //Act
            var result=await orderdetails.Index();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<OrderDetailTbl>>();
            dbcontext.OrderDetails.Should().HaveCount(2);
        }
        [Fact]
        public async Task DetailsTests()
        {
            //Arrange
            var inmemory = new DemoInMemoryDatabase();
            var dbcontext=await inmemory.GetDatabaseContext();
            var orderdetails = new OrderDetailsController(dbcontext);
            OrderDetailTbl res = new OrderDetailTbl();

            //Act
            var result =await orderdetails.Details(601);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<OrderDetailTbl>>();
        }
        [Fact]
        public async Task CancelTests()
        {
            //Arrange
            var inmemory = new DemoInMemoryDatabase();
            var dbcontext=await inmemory.GetDatabaseContext();
            var orderdetails=new OrderDetailsController(dbcontext);

            //Act
            var result = await orderdetails.Cancel(601);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<OrderDetailTbl>>();
            dbcontext.OrderDetails.Should().HaveCount(1);
        }
    }
}
