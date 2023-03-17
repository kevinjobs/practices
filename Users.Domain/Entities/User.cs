namespace Users.Domain
{
    public record User : IAggregateRoot
    {
        public Guid Id { get; set; }
        public PhoneNumber PhoneNumber { get; private set; }
        private string? passwordHash;
        public UserAccessFail AccessFail { get; private set; }
        private User()
        {
            //
        }

        public User(PhoneNumber phoneNumber)
        {
            Id = Guid.NewGuid();
            PhoneNumber = phoneNumber;
            this.AccessFail = new UserAccessFail(this);
        }

        public bool HasPassword()
        {
            return !string.IsNullOrEmpty(passwordHash);
        }

        public void ChangePassword(string value)
        {
            if (value.Length <= 8)
            {
                throw new ArgumentException("Password length must > 8");
            }
            passwordHash = HashHelper.ComputeMd5Hash(value);
        }

        public bool CheckPassword(string password)
        {
            return passwordHash == HashHelper.ComputeMd5Hash(password);
        }

        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}