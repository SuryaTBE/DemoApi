using DemoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredentialsController : ControllerBase
    {
        private readonly DemoContext _dc;
        public CredentialsController(DemoContext dc)
        {
            _dc = dc;
        }
        [HttpPost]
        public async Task<ActionResult<UserTbl>> Register(UserTbl usertbl)
        {
            _dc.UserTbls.Add(usertbl);
            await _dc.SaveChangesAsync();
            return Ok(usertbl);
        }
        [HttpPost]
        [Route("UserLogin")]
        public async Task<ActionResult<UserTbl>> UserLogin(UserTbl usertbl)
        {
            try
            {
                var result = (from i in _dc.UserTbls
                              where i.Email==usertbl.Email && i.Password==usertbl.Password
                              select i).FirstOrDefault();
                if(result==null)
                {
                    return BadRequest("Invalid Credentials");
                }
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Wrong Entry");
            }
        }
        [HttpPost]
        [Route("AdminLogin")]
        public async Task<ActionResult<AdminTbl>> AdminLogin(AdminTbl admintbl)
        {
            try 
            {
                var result = (from i in _dc.AdminTbls
                              where i.Email == admintbl.Email && i.Password == admintbl.Password
                              select i).FirstOrDefault();
                if(result == null)
                {
                    return BadRequest("Invalid Credentials");
                }
                return result;
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Wrong Entry");
            }
        }

    }
}
