using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Robson_Totvs_Test.Application.DTO.Models.Common;
using Robson_Totvs_Test.Application.DTO.Models.Request;
using Robson_Totvs_Test.Application.DTO.Models.Response;
using Robson_Totvs_Test.Domain.Entities;
using Robson_Totvs_Test.Domain.Interfaces.Repositories;
using Robson_Totvs_Test.Domain.Interfaces.Services;
using Robson_Totvs_Test.Filters;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Robson_Totvs_Test.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        UserManager<Account> _userManager;
        IAccountRepository _accountRepository;
        ITotvsTokenService _tokenService;

        public AccountController(
            ITotvsTokenService tokenService,
            IAccountRepository accountRepository,
            UserManager<Account> userManager,
            SignInManager<Account> signInManager)
        {
            this._userManager = userManager;
            this._accountRepository = accountRepository;
            this._tokenService = tokenService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody][FromForm][FromQuery][Required] CreateAccountDTO request)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
                return StatusCode(
                    statusCode: (int)HttpStatusCode.BadRequest,
                    value: new GetErrorResponseDTO(HttpStatusCode.BadRequest, "This email already exists."));

            var myProfiles = request.Profiles.Select(x => new ProfileObject(x.Type, null)).ToList();
            var myNewAccount = new Account(request.Name, request.Email, myProfiles);

            var createAccountResult = await this._userManager.CreateAsync(myNewAccount, request.Password);

            if (createAccountResult.Succeeded == false)
            {
                if (createAccountResult.Errors.Any())
                    return BadRequest(createAccountResult);

                return StatusCode(
                    statusCode: (int)(HttpStatusCode.InternalServerError),
                    value: new GetErrorResponseDTO(HttpStatusCode.InternalServerError, "Error creating user."));
            }

            var token = await _tokenService.GenerateTokenAsync(myNewAccount.UserName);
            var profilesDto = myProfiles.Select(x => new ProfileObjectDTO(x.Type)).ToList();

            var result = new GetAccountResponseDTO(
                token,
                myNewAccount.PasswordHash,
                myNewAccount.Name,
                myNewAccount.Created,
                myNewAccount.Modified,
                myNewAccount.LastLogin,
                profilesDto);
            
            return Ok(result);
        }

        [HttpGet("/account")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] GetAccountFilterDTO request)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var myQuery = AccountFilter.GetSqlCommandByRequest(request);

            var myAccounts = await _accountRepository.FindAllAsyncWithDapperAsync(myQuery);

            return Ok(myAccounts);
        }
    }
}
