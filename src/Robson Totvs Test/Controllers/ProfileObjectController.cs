﻿using Microsoft.AspNetCore.Mvc;
using Robson_Totvs_Test.Application.DTO.Models.Common;
using Robson_Totvs_Test.Application.DTO.Models.Response;
using Robson_Totvs_Test.Domain.Entities;
using Robson_Totvs_Test.Domain.Interfaces.Repositories;
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
        public async Task<IActionResult> RegisterProfile([FromRoute][Required] string userId, [FromBody][Required] ProfileObjectDTO request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var user = await _accountRepository.FindAsync(x => x.Id == userId);

            if (user == null)
            {
                return StatusCode(
                    statusCode: (int)HttpStatusCode.NotFound,
                    value: new GetErrorResponseDTO(HttpStatusCode.NotFound, "User not found with the resquested id."));
            }
            else if(user.Profiles.Any(x=>x.Type == request.Type))
            {
                return StatusCode(
                    statusCode: (int)HttpStatusCode.BadRequest,
                    value: new GetErrorResponseDTO(HttpStatusCode.BadRequest, "Profile type must be unique"));
            }

            var myNewProfile = new ProfileObject(request.Type, user.Id);
            
            var success = await _profileObjectRepository.AddAsync(myNewProfile);

            if (success == false)
            {
                return StatusCode(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    value: new GetErrorResponseDTO(HttpStatusCode.InternalServerError, "Error while saving profile-object on database"));
            }

            return Ok(myNewProfile);
        }
    }
}
