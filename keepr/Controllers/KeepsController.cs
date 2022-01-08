using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _ks;
        public KeepsController(KeepsService ks)
        {
            _ks = ks;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newKeep.CreatorId = userInfo.Id;
                Keep keep = _ks.Create(newKeep);
                keep.Creator = userInfo;
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // FIXME Get all is not working properly
        // [HttpGet]
        // public ActionResult<List<Keep>> GetAll()
        // {
        //     try
        //     {
        //         List<Keep> keeps = _ks.GetAll();
        //         return Ok(keeps);
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }

        [HttpGet("{id}")]
        public ActionResult<Keep> GetByKeepId(int id)
        {
            try
            {
                Keep keep = _ks.GetByKeepId(id);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Keep>> Edit([FromBody] Keep update, int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                update.CreatorId = userInfo.Id;
                update.Id = id;
                Keep updated = _ks.Edit(update);
                return Ok(updated);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<String>> Delete(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _ks.Delete(id, userInfo.Id);
                return Ok("Successfully Removed a Keep");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}