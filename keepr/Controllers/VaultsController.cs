using System;
using System.Collections.Generic;
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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;
        private readonly VaultKeepsService _vks;


        public VaultsController(VaultsService vs, VaultKeepsService vks)
        {
            _vs = vs;
            _vks = vks;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newVault.CreatorId = userInfo.Id;
                Vault vault = _vs.Create(newVault);
                vault.Creator = userInfo;
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> Edit([FromBody] Vault update, int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                update.CreatorId = userInfo.Id;
                update.Id = id;
                Vault updated = _vs.Edit(update, userInfo.Id);
                return Ok(updated);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // TODO allow users not logged in to get vaults
        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> GetByVaultId(int id)
        {

            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault vault = _vs.GetByVaultId(id, userInfo.Id);
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<VaultKeep>>> GetKeepsByVaultId(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<VaultKeep> vaultKeep = _vks.GetKeepsByVaultId(id, userInfo.Id);
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
                _vs.Delete(id, userInfo.Id);
                return Ok("Successfully Deleted Vault");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
