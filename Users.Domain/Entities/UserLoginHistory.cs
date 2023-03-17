namespace Users.Domain
{
    public record UserLoginHistory : IAggregateRoot
    {
        public long Id { get; init; }
        public Guid? UserId { get; init; }
        public PhoneNumber PhoneNumber { get; init; }
        public DateTime CreateDateTime { get; init; }
        public string Message { get; init; }
        private UserLoginHistory() { }
        public UserLoginHistory(Guid? userId, PhoneNumber phoneNumber, string message)
        {
            this.UserId = userId;
            this.PhoneNumber = phoneNumber;
            this.CreateDateTime = DateTime.Now;
            this.Message = message;
        }
    }
}