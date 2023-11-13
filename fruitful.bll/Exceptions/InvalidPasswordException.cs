namespace fruitful.bll.Exceptions;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException()
        : base("The provided password does not match our records.")
    {
    }
}