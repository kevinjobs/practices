namespace Users.WebApi.Controllers
{
    public record ChangePasswordRequest(Guid Id, string Password);
}