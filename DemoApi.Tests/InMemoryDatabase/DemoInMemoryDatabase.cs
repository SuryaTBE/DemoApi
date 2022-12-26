using Microsoft.EntityFrameworkCore;
using DemoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Tests.InMemoryDatabase
{
    public class DemoInMemoryDatabase
    {
        public async Task<DemoContext> GetDatabaseContext()
        {
            var options=new DbContextOptionsBuilder<DemoContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbcontext=new DemoContext(options);
            dbcontext.Database.EnsureCreated();
            int uid = 200;
            int mid = 300;
            int bid = 400;
            int omid = 500;
            int odid = 600;
            dbcontext.AdminTbls.Add(
                new AdminTbl()
                {
                    AdminId = 101,
                    AdminName = "Surya T",
                    Email= "1234@gmail.com",
                    Password="1234"
                });
            dbcontext.Add(
                new UserTbl()
                {
                    UserId = uid++,
                    UserName = "Sagar",
                    Email = "12@gmail.com",
                    Password = "1234"

                });
            dbcontext.Add(
                new UserTbl()
                {
                    UserId = uid++,
                    UserName = "Akash",
                    Email = "34@gmail.com",
                    Password = "1234"
                });
            await dbcontext.SaveChangesAsync();
            for(int i=1;i<3;i++)
            {
                dbcontext.Add(
                    new MovieTbl()
                    {
                        MovieId = mid + i,
                        MovieName = "Avatar"+i,
                        Image = "In the Local file",
                        Date = new DateTime(2022, 12, 25),
                        Slot = "10:30AM",
                        Cost = 150,
                        capacity = 50
                    });
                dbcontext.Add(
                    new BookingTbl()
                    {
                        BookingId = bid + i,
                        MovieId = mid + i,
                        MovieName = "Avatar" + i,
                        Date = new DateTime(2022, 12, 25),
                        Slot = "10:30AM",
                        UserId = uid + i,
                        NoOfTickets = 2,
                        SeatNo = "1,2",
                        AmountTotal = 2 * 150
                    });
                dbcontext.Add(
                    new OrderMasterTbl()
                    {
                        OrderMasterId = omid + i,
                        UserId = uid + i,
                        OrderDate = DateTime.Now,
                        CardNo=123+i,
                        Amount=300,
                        Paid=300
                    });
                dbcontext.Add(
                    new OrderDetailTbl()
                    {
                        OrderDetailId = odid + i,
                        MovieName = "Avatar" + i,
                        UserId = uid + i,
                        MovieId = mid + i,
                        MovieDate = new DateTime(2022, 12, 25),
                        OrderMasterId = omid + i,
                        Slot = "10:30AM",
                        NoOfTickets = 2,
                        SeatNo = "1,2",
                        Cost = 2 * 150
                    });
                await dbcontext.SaveChangesAsync();
            }
            return dbcontext;
        }
    }
}
