using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Robson_Totvs_Test.Application.DTO.Models.Request;
using Robson_Totvs_Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Robson_Totvs_Test.Controllers
{
    [Route("[controller]")]
    public class ProfileObjectController : ControllerBase
    {

        UserManager<Account> _userManager;

        public ProfileObjectController(
            UserManager<Account> userManager
            )
        {
            this._userManager = userManager;
        }

        [HttpPost("/account/{userId}/profile-object")]

        public async Task<IActionResult> RegisterProfile([FromRoute][Required] string userId, [FromBody][Required] CreateObjectProfileRequestDTO request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found with the resquested id");

            }
            var myNewProfile = new ProfileObject(request.Type);
            user.Profiles.Add(myNewProfile);

            var updateUserResult = await _userManager.UpdateAsync(user);

            if (updateUserResult.Succeeded == false)
            {

                var myError = updateUserResult.Errors.FirstOrDefault();

                return Problem(statusCode: (int)HttpStatusCode.InternalServerError, detail: myError.Description);

            }

            return Ok(myNewProfile);

        }

    }
}
