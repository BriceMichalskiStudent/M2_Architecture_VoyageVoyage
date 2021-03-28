using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;

namespace MicroService.User.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Test : ControllerBase
    {
        [HttpGet]
        [Route("GetAuth")]
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
