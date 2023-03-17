namespace Users.Domain
{
    public enum UserAccessResult
    {
        OK,
        PhoneNumberNotFound,
        LockOut,
        NoPassword,
        PasswordError
    }
}