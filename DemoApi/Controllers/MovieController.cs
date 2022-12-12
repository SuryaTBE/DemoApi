using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoApi.Models;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly DemoContext _context;

        public MovieController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/Movie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieTbl>>> GetMovieTbls()
        {
            return await _context.MovieTbls.ToListAsync();
        }

        // GET: api/Movie/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieTbl>> GetMovieTbl(int id)
        {
            var movieTbl = await _context.MovieTbls.FindAsync(id);

            if (movieTbl == null)
            {
                return NotFound();
            }

            return movieTbl;
        }

        // PUT: api/Movie/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieTbl(int id, MovieTbl movieTbl)
        {
            if (id != movieTbl.MovieId)
            {
                return BadRequest();
            }

            _context.Entry(movieTbl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieTblExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movie
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieTbl>> PostMovieTbl(MovieTbl movieTbl)
        {
            _context.MovieTbls.Add(movieTbl);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovieTbl", new { id = movieTbl.MovieId }, movieTbl);
        }

        // DELETE: api/Movie/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieTbl(int id)
        {
            var movieTbl = await _context.MovieTbls.FindAsync(id);
            if (movieTbl == null)
            {
                return NotFound();
            }

            _context.MovieTbls.Remove(movieTbl);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieTblExists(int id)
        {
            return _context.MovieTbls.Any(e => e.MovieId == id);
        }
    }
}
