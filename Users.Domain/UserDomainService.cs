using Users.Domain.Events;
using Users.Domain;

namespace Users.Domain
{
    public class UserDomainService
    {
        private readonly IUserDomainRepository repository;
        private readonly ISmsCodeSender smsSender;

        public UserDomainService(IUserDomainRepository repository, ISmsCodeSender smsSender)
        {
            this.repository = repository;
            this.smsSender = smsSender;
        }

        public async Task<UserAccessResult> CheckLoginAsync(PhoneNumber phoneNum,
            string password)
        {
            User? user = await repository.FindOneAsync(phoneNum);
            UserAccessResult result;

            if (user == null)
            {
                result = UserAccessResult.PhoneNumberNotFound;
            }
            else if (IsLockOut(user))
            {
                result = UserAccessResult.LockOut;
            }
            else if (user.HasPassword() == false)
            {
                result = UserAccessResult.NoPassword;
            }
            else if (user.CheckPassword(password))
            {
                result = UserAccessResult.OK;
            }
            else
            {
                result = UserAccessResult.PasswordError;
            }

            if (user != null)
            {
                if (result == UserAccessResult.OK)
                {
                    this.ResetAccessFail(user);
                }
                else
                {
                    this.AccessFail(user);
                }
            }

            UserAccessResultEvent eventItem = new(phoneNum, result);
            await repository.PublishEventAsync(eventItem);

            return result;
        }

        public async Task<UserAccessResult> SendCodeAsync(PhoneNumber phoneNum)
        {
            var user = await repository.FindOneAsync(phoneNum);
            if (user == null)
            {
                return UserAccessResult.PhoneNumberNotFound;
            }

            if (IsLockOut(user))
            {
                return UserAccessResult.LockOut;
            }

            string code = Random.Shared.Next(1000, 9999).ToString();
            await repository.SavePhoneCodeAsync(phoneNum, code);
            await smsSender.SendCodeAsync(phoneNum, code);
            return UserAccessResult.OK;
        }

        public async Task<CheckCodeResult> CheckCodeAsync(PhoneNumber phoneNum, string code)
        {
            var user = await repository.FindOneAsync(phoneNum);
            if (user == null)
            {
                return CheckCodeResult.PhoneNumberNotFound;
            }

            if (IsLockOut(user))
            {
                return CheckCodeResult.Lockout;
            }

            string? codeInServer = await repository.RetrievePhoneCodeAsync(phoneNum);
            if (string.IsNullOrEmpty(codeInServer))
            {
                return CheckCodeResult.CodeError;
            }
            if (code == codeInServer)
            {
                return CheckCodeResult.OK;
            }
            else
            {
                AccessFail(user);
                return CheckCodeResult.CodeError;
            }
        }

        public void ResetAccessFail(User user)
        {
            user.AccessFail.Reset();
        }

        public bool IsLockOut(User user)
        {
            return user.AccessFail.IsLockOut();
        }

        public void AccessFail(User user)
        {
            user.AccessFail.Fail();
        }
    }
}