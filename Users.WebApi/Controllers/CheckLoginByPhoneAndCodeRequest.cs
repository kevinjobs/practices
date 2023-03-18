using Users.Domain;

namespace Users.WebApi.Controllers
{
    public record CheckLoginByPhoneAndCodeRequest(
        PhoneNumber PhoneNumber, string Code);
}