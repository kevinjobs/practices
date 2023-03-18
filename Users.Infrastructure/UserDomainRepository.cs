using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Users.Domain;
using Users.Domain.Events;
using static Users.Infrastructure.ExpressionHelper;

namespace Users.Infrastructure
{
    public class UserDomainRepository : IUserDomainRepository
    {
        private readonly UserDbContext _context;
        private readonly IDistributedCache _cache;
        private readonly IMediator _mediator;

        public UserDomainRepository(UserDbContext context, IDistributedCache cache, IMediator mediator)
        {
            this._context = context;
            this._cache = cache;
            this._mediator = mediator;
        }

        public Task<User?> FindOneAsync(PhoneNumber phoneNumber)
        {
            return _context.Users.Include(u => u.AccessFail).SingleOrDefaultAsync(MakeEqual((User u) => u.PhoneNumber, phoneNumber));
        }

        public Task<User?> FindOneAsync(Guid userId)
        {
            return _context.Users.Include(u => u.AccessFail).SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber, string msg)
        {
            var user = await FindOneAsync(phoneNumber);
            UserLoginHistory history = new UserLoginHistory(user?.Id, phoneNumber, msg);
            _context.LoginHistories.Add(history);
            // don't save here.
        }

        public Task<string?> RetrievePhoneCodeAsync(PhoneNumber phoneNumber)
        {
            string fullNumber = phoneNumber.RegionCode + phoneNumber.Number;
            string cacheKey = $"LoginByPhoneAndCode_Code_{fullNumber}";
            string? code = _cache.GetString(cacheKey);
            _cache.Remove(cacheKey);
            return Task.FromResult(code);
        }

        public Task PublishEventAsync(UserAccessResultEvent eventData)
        {
            return _mediator.Publish(eventData);
        }

        public Task SavePhoneCodeAsync(PhoneNumber phoneNumber, string code)
        {
            string fullNumber = phoneNumber.RegionCode + phoneNumber.Number;
            var opts = new DistributedCacheEntryOptions();
            opts.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
            _cache.SetString($"LoginByPhoneAndCode_Code_{fullNumber}", code, opts);
            return Task.CompletedTask;
        }
    }
}