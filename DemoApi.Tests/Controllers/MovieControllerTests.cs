using DemoApi.Controllers;
using DemoApi.Models;
using DemoApi.Tests.InMemoryDatabase;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace DemoApi.Tests.Controllers
{
    public class MovieControllerTests
    {
        [Fact]
        public async Task GetMovieTblsTests()
        {
            //Arrange
            var inmemory = new DemoInMemoryDatabase();
            var dbcontext = await inmemory.GetDatabaseContext();
            var movie=new MovieController(dbcontext);

            //Act
            var result=await movie.GetMovieTbls();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<IEnumerable<MovieTbl>>>();
            result.Value.Should().HaveCount(2);

        }
        [Fact]
        public async Task SearchTests()
        {
            //Arrange
            var inmemory = new DemoInMemoryDatabase();
            var dbcontext=await inmemory.GetDatabaseContext();
            var movie = new MovieController(dbcontext);
            MovieTbl movieTbl= new MovieTbl()
            {
                Date=Convert.ToDateTime("25/12/2022")
            };

            //Act
            var result=await movie.Search(movieTbl);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<MovieTbl>>();
        }
        [Fact]
        public async Task DetailsTests()
        {
            //Arrange
            var inmemory = new DemoInMemoryDatabase();
            var dbcontext= await inmemory.GetDatabaseContext();
            var movie= new MovieController(dbcontext);

            //Act
            var result = await movie.Details(301);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<MovieTbl>>();
        }
        [Fact]
        public async Task PutMovieTblTests()
        {
            //Arrange
            var inmemory=new DemoInMemoryDatabase();
            var dbcontext = await inmemory.GetDatabaseContext();
            var movie=new MovieController(dbcontext);
            MovieTbl movieTbl = new MovieTbl()
            {
                MovieId = 301,
                MovieName = "Avatar1",
                Image = "In the Local file",
                Date = new DateTime(2022, 12, 25),
                Slot = "10:30AM",
                Cost = 200,
                capacity = 50
            };

            //Act
            var m = await dbcontext.MovieTbls.FindAsync(movieTbl.MovieId);
            var result=await movie.PutMovieTbl(m);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
            dbcontext.MovieTbls.Should().HaveCount(2);
        }
        [Fact]
        public async Task PostMovieTblTests()
        {
            //Arrange
            var inmemory= new DemoInMemoryDatabase();
            var dbcontext=await inmemory.GetDatabaseContext(); ;
            var movie=new MovieController(dbcontext);
            MovieTbl movieTbl = new MovieTbl()
            {
                MovieId = 303,
                MovieName = "Varisu",
                Image = "File Found Local",
                Date = new DateTime(2022, 12, 25),
                Slot = "6:30PM",
                Cost = 200,
                capacity = 50
            };

            //Act
            var result=await movie.PostMovieTbl(movieTbl);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<MovieTbl>>();
            dbcontext.MovieTbls.Should().HaveCount(3);
        }
        [Fact]
        public async Task DeleteMovieTblTests()
        {
            //Arrange
            var inmemory= new DemoInMemoryDatabase();
            var dbcontext=await inmemory.GetDatabaseContext();
            var movie=new MovieController(dbcontext);

            //Act
            var result = await movie.DeleteMovieTbl(301);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
            dbcontext.MovieTbls.Should().HaveCount(1);

       }
    }
}
