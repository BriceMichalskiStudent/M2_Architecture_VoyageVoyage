using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FirebaseAdmin.Auth;

using Microsoft.AspNetCore.Authorization;

namespace MicroService.User.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        [Route("GetNoAuth")]
        public ActionResult Get()
        {
            try
            {
                return Ok("no auth");
            }
            catch (Exception e)
            {
                return Problem("Une erreur a eu lieu.");
            }
        }

        [HttpGet]
        [Route("GetAuth")]
        [Authorize]
        public ActionResult GetAuth()
        {
            try
            {
                return Ok("auth");
            }
            catch (Exception e)
            {
                return Problem("Une erreur a eu lieu.");
            }
        }
    }
}
