using DemoApi.Controllers;
using DemoApi.Models;
using DemoApi.Tests.InMemoryDatabase;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Tests.Controllers
{
    public class CredentialsControllerTests
    {
        [Fact]
        public async Task AdminLoginTests()
        {
            //Arrange
            var inmemory = new DemoInMemoryDatabase();
            var dbcontext=await inmemory.GetDatabaseContext();
            var credentials = new CredentialsController(dbcontext);
            AdminTbl admin=new AdminTbl()
            {
                Email = "1234@gmail.com",
                Password = "1234"
            };

            //Act
            var result = await credentials.AdminLogin(admin);

            //Assert
            result.Value.Should().NotBeNull();
            dbcontext.AdminTbls.Should().HaveCount(1);
        }
        [Fact]
        public async Task RegisterTests()
        {
            //Arrange
            var inmemory=new DemoInMemoryDatabase();
            var dbcontext = await inmemory.GetDatabaseContext();
            var credentials=new CredentialsController(dbcontext);
            UserTbl user = new UserTbl()
            {
                UserId = 203,
                UserName = "Rithick",
                Email = "56@gmail.com",
                Password = "1234"
            };

            //Act
            var result= await credentials.Register(user);

            //Assert
            //result.Value.Should().NotBeNull();
            dbcontext.UserTbls.Should().Contain(user);
            dbcontext.UserTbls.Should().HaveCount(3);
        }
        [Fact]
        public async Task UserLoginTests()
        {
            //Arrange
            var inmemory=new DemoInMemoryDatabase();
            var dbcontext= await inmemory.GetDatabaseContext();
            var credential = new CredentialsController(dbcontext);
            UserTbl user = new UserTbl()
            {
                Email = "12@gmail.com",
                Password = "1234"
            };

            //Act
            var result=await credential.UserLogin(user);

            //Assert
            result.Value.Should().NotBeNull();
            dbcontext.UserTbls.Should().Contain(result.Value);
        }
    }
}
