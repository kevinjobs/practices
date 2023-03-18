using Microsoft.AspNetCore.Mvc;
using Users.Domain;
using Users.Infrastructure;

namespace Users.WebApi.Controllers.Login
{
    [Route("[controller]/[action]")]
    [ApiController]
    [UnitOfWork(typeof(UserDbContext))]
    public class LoginController : ControllerBase
    {
        private readonly UserDomainService _service;

        public LoginController(UserDomainService service)
        {
            _service = service;
        }

        [HttpPut]
        public async Task<IActionResult> LoginByPhoneAndPwd(LoginByPhoneAndPwdRequest request)
        {
            if (request.Password.Length < 3)
            {
                return BadRequest("password is less than 3");
            }

            var phoneNum = request.PhoneNumber;
            var result = await _service.CheckLoginAsync(phoneNum, request.Password);
            switch (result)
            {
                case UserAccessResult.OK:
                    return Ok("login success");
                case UserAccessResult.PhoneNumberNotFound:
                    // even if there is not phone number or password
                    // we return this response to avoid leaking.
                    return BadRequest("phone number or password is wrong");
                case UserAccessResult.LockOut:
                    return BadRequest("user is lockout, try later.");
                case UserAccessResult.NoPassword:
                case UserAccessResult.PasswordError:
                    return BadRequest("phone number or password is wrong");
                default:
                    throw new NotImplementedException();
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendCodeByPhone(SendLoginByPhoneAndCodeRequest request)
        {
            var result = await _service.SendCodeAsync(request.PhoneNumber);
            switch(result)
            {
                case UserAccessResult.OK:
                    return Ok("code has been sent");
                case UserAccessResult.LockOut:
                    return BadRequest("user has been lockout, try later.");
                default:
                    return BadRequest("bad request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckCode(CheckLoginByPhoneAndCodeRequest request)
        {
            var result = await _service.CheckCodeAsync(request.PhoneNumber, request.Code);
            switch (result)
            {
                case CheckCodeResult.OK:
                    return Ok("login success");
                case CheckCodeResult.PhoneNumberNotFound:
                    return BadRequest("bad request");
                case CheckCodeResult.Lockout:
                    return BadRequest("user has been lockout, try later.");
                case CheckCodeResult.CodeError:
                    return BadRequest("code is wrong.");
                default:
                    throw new NotImplementedException();
            }
        }
    }
}