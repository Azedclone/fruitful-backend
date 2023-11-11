namespace fruitful.bll.Services;

public class EncryptionService
{
    private const int SALT_ROUND = 10;
    private readonly string _salt;

    public EncryptionService()
    {
        _salt = BCrypt.Net.BCrypt.GenerateSalt(SALT_ROUND);
    }

    public string EncryptPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, _salt);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}