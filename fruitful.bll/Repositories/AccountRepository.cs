using fruitful.bll.Exceptions;
using fruitful.bll.Services;
using fruitful.dal.Collections;
using fruitful.dal.Models;
using MongoDB.Driver;

namespace fruitful.bll.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly IMongoCollection<Account> _accounts;
    private readonly EncryptionService _encryptionService;

    public AccountRepository(IMongoDatabase db, EncryptionService encryptionService)
    {
        _accounts = db.GetCollection<Account>(nameof(Account));
        _encryptionService = encryptionService;
    }


    public async Task<OperationResult<Account>> Register(RegistrationBody body)
    {
        var filter = Builders<Account>.Filter.Or(
            Builders<Account>.Filter.Eq(a => a.Username, body.Username),
            Builders<Account>.Filter.Eq(a => a.Phone, body.Phone)
        );

        var isDuplicate = await _accounts.Find(filter).FirstOrDefaultAsync() != null;

        if (isDuplicate)
        {
            throw new DuplicateAccountException("Account with the given username or phone number already exists");
        }

        var account = new Account
        {
            Username = body.Username,
            Password = _encryptionService.EncryptPassword(body.Password),
            Phone = body.Phone,
            Address = body.Address,
            Dob = body.Dob
        };
        await _accounts.InsertOneAsync(account);
        return new OperationResult<Account>
        {
            Success = true,
            Data = new Account
                { Username = account.Username, Phone = account.Phone, Address = account.Address, Dob = account.Dob }
        };
    }
}