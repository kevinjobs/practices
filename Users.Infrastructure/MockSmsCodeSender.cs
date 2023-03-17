using Users.Domain;

namespace Users.Infrastructure;

public class MockSmsCodeSender : ISmsCodeSender
{
    private readonly ILogger<MockSmsCodeSender> _logger;

    public MockSmsCodeSender(ILogger<MockSmsCodeSender> logger)
    {
        _logger = logger;
    }

    public Task SendCodeAsync(PhoneNumber phoneNumber, string code)
    {
        _logger.LogInformation($"send code: {code} to {phoneNumber}");
        return Task.CompletedTask;
    }
}