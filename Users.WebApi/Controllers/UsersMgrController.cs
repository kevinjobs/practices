using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Users.Domain;
using Users.Infrastructure;

namespace Users.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [UnitOfWork(typeof(UserDbContext))]
    public class UsersMgrController : ControllerBase
    {
        private readonly UserDbContext _dbCtx;
        private readonly UserDomainService _domainService;
        private readonly IUserDomainRepository _repository;

        public UsersMgrController(UserDbContext dbCtx, UserDomainService domainService, IUserDomainRepository repository)
        {
            _dbCtx = dbCtx;
            _domainService = domainService;
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(PhoneNumber req)
        {
            if ((await _repository.FindOneAsync(req)) != null)
            {
                return BadRequest("phone number existed.");
            }
            User user = new User(req);
            _dbCtx.Users.Add(user);
            return Ok("add new user success");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest req)
        {
            var user = await _repository.FindOneAsync(req.Id);
            if (user == null)
            {
                return NotFound();
            }
            user.ChangePassword(req.Password);
            return Ok("change password success.");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Unlock(Guid id)
        {
            var user = await _repository.FindOneAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _domainService.ResetAccessFail(user);
            return Ok("unlock access fail success");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _dbCtx.Users.ToListAsync();
            return Ok(users);
        }
    }
}