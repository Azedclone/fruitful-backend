namespace fruitful.bll.Exceptions;

public class AccountNotFoundException : Exception
{
    public AccountNotFoundException(string username)
        : base($"Account with username '{username}' was not found.")
    {
    }
}