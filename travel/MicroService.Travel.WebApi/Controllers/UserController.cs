using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FirebaseAdmin.Auth;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;

namespace MicroService.Travel.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        [Authorize]
        [HttpGet]
        [Route("current")]
        public async Task<ActionResult> GetCurrent()
        {
            string accessToken = Request.Headers[HeaderNames.Authorization];

            var jwtToken = accessToken.Replace("Bearer ", string.Empty);

            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(jwtToken);
            var user = await FirebaseAuth.DefaultInstance.GetUserAsync(decodedToken.Uid);
            return Ok(user);
        }
    }
}
