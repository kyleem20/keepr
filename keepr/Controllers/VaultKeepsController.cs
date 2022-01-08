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
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vks;


        public VaultKeepsController(VaultKeepsService vks)
        {
            _vks = vks;

        }
        // TODO fix this Create, not working
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVK)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newVK.CreatorId = userInfo.Id;
                VaultKeep vaultKeep = _vks.Create(newVK);
                vaultKeep.Creator = userInfo;
                return Ok(newVK);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<VaultKeep> GetByVKId(int id)
        {
            try
            {
                VaultKeep vaultKeep = _vks.GetByVaultKeepId(id);
                return Ok(vaultKeep);
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
                _vks.Delete(id, userInfo.Id);
                return Ok("Successfully Deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}