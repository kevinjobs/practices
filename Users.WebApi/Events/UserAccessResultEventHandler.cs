using MediatR;
using Users.Domain;
using Users.Domain.Events;

namespace Users.WebApi.Events;

public class UserAccessResultEventHandler
    : INotificationHandler<UserAccessResultEvent>
{
    private readonly IUserDomainRepository _repository;

    public UserAccessResultEventHandler(IUserDomainRepository repository)
    {
        this._repository = repository;
    }

    public Task Handle(UserAccessResultEvent notification, CancellationToken cancellationToken)
    {
        var result = notification.Result;
        var phoneNum = notification.PhoneNumber;
        string msg;
        switch (result)
        {
            case UserAccessResult.OK:
                msg = $"{phoneNum} login success";
                break;
            case UserAccessResult.PhoneNumberNotFound:
                msg = $"{phoneNum} user doesn't exist";
                break;
            case UserAccessResult.PasswordError:
                msg = $"{phoneNum} password is error";
                break;
            case UserAccessResult.NoPassword:
                msg = $"{phoneNum} password hasn't been set";
                break;
            case UserAccessResult.LockOut:
                msg = $"{phoneNum} is lockout";
                break;
            default:
                throw new NotImplementedException();
        }
        return _repository.AddNewLoginHistoryAsync(phoneNum, msg);
    }
}