using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly VaultsService _vs;
        private readonly KeepsService _ks;
        private readonly AccountService _acts;



        public ProfilesController(VaultsService vs, KeepsService ks, AccountService acts)
        {
            _vs = vs;
            _ks = ks;
            _acts = acts;
        }
        // [HttpGet("{id}")]
        // public async Task<ActionResult<Profile>> GetByProfileId()
        // {
        //     try
        //     {
        //         Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        //         return Ok(_acts.GetOrCreateProfile(userInfo));
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }

        [HttpGet("{id}/vaults")]
        public ActionResult<List<Vault>> GetVaults(string id)
        {
            try
            {
                List<Vault> vaults = _vs.GetByCreatorId(id);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // NOTE Why doesn't this work properly?
        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetKeeps(string id)
        {
            try
            {
                List<Keep> keeps = _ks.GetByCreatorId(id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}