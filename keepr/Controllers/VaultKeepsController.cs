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

    }
}