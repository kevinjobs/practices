using Users.Domain;

namespace Users.WebApi.Controllers
{
    public record LoginByPhoneAndPwdRequest(PhoneNumber PhoneNumber, string Password);
}