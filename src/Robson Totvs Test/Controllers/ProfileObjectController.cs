using Microsoft.AspNetCore.Mvc;
using Robson_Totvs_Test.Application.DTO.Models.Request;
using Robson_Totvs_Test.Data.Repositories;
using Robson_Totvs_Test.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Robson_Totvs_Test.Controllers
{
    [Route("[controller]")]
    public class ProfileObjectController : ControllerBase
    {
        IAccountRepository _accountRepository;
        IProfileObjectRepository _profileObjectRepository;

        public ProfileObjectController(
            IAccountRepository accountRepository,
            IProfileObjectRepository profileObjectRepository)
        {
            _accountRepository = accountRepository;
            _profileObjectRepository = profileObjectRepository;
        }

        [HttpPost("/account/{userId}/profile-object")]
        public async Task<IActionResult> RegisterProfile([FromRoute][Required] string userId, [FromBody][Required] CreateObjectProfileRequestDTO request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var user = await _accountRepository.FindAsync(x => x.Id == userId);

            if (user == null)
            {
                return NotFound("User not found with the resquested id");
            }
            else if(user.Profiles.Any(x=>x.Type == request.Type))
            {
                return BadRequest("Profile type must be unique");
            }

            var myNewProfile = new ProfileObject(request.Type, user.Id);
            
            var success = await _profileObjectRepository.AddAsync(myNewProfile);

            if (success == false)
            {
                return Problem(statusCode: (int)HttpStatusCode.InternalServerError, detail: "Error while saving profile-object on database");
            }

            return Ok(myNewProfile);
        }
    }
}
