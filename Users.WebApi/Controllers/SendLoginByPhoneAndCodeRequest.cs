using Users.Domain;

namespace Users.WebApi.Controllers
{
    public record SendLoginByPhoneAndCodeRequest(PhoneNumber PhoneNumber);
}