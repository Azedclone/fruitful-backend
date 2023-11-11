namespace fruitful.bll.Exceptions;

public class DuplicateAccountException : Exception
{
    public DuplicateAccountException(string message) : base(message)
    {
    }
}