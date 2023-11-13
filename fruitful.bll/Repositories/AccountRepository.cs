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

    public async Task<OperationResult<Account>> Login(LoginBody body)
    {
        var filter = Builders<Account>.Filter.Eq(a => a.Username, body.Username);

        // Fetch the account only once from the database
        var account = await _accounts.Find(filter).FirstOrDefaultAsync();
        if (account == null) throw new AccountNotFoundException(body.Username);

        // Verify the password
        var isPasswordValid = _encryptionService.VerifyPassword(body.Password, account.Password);
        if (!isPasswordValid) throw new InvalidPasswordException();
        return new OperationResult<Account>
        {
            Success = true,
            Data = account
        };
    }

    public async Task<OperationResult<List<Account>>> GetAccounts()
    {
        try
        {
            // Fetch all accounts from the database
            var accountsList = await _accounts.Find(a => a.Role.Equals("EMPLOYEE")).ToListAsync();
            Console.WriteLine(accountsList[0]);

            // If the list is not empty, return it with a success status
            return new OperationResult<List<Account>>
            {
                Success = true,
                Data = accountsList
            };
        }
        catch (Exception ex)
        {
            return new OperationResult<List<Account>>
            {
                Success = false,
                Data = null,
            };
        }
    }

    public async Task<OperationResult<Account>> GetAccountById(string id)
    {
        try
        {
            // Build a filter to find the account by id
            var filter = Builders<Account>.Filter.Eq(a => a.Id, id);

            // Fetch the account from the database
            var account = await _accounts.Find(filter).FirstOrDefaultAsync();
            if (account == null)
            {
                // Handle the case where the account doesn't exist
                return new OperationResult<Account>
                {
                    Success = false,
                    Data = null
                };
            }

            // If the account is found, return it with a success status
            return new OperationResult<Account>
            {
                Success = true,
                Data = account
            };
        }
        catch (Exception ex)
        {
            // Handle any other exceptions that occur during the database operation
            return new OperationResult<Account>
            {
                Success = false,
                Data = null
            };
        }
    }
    
    public async Task<OperationResult<Account>> UpdateAccountActive(string id, bool isActive)
    {
        try
        {
            // Build a filter to find the account by id
            var filter = Builders<Account>.Filter.Eq(a => a.Id, id);
            var update = Builders<Account>.Update.Set(a => a.IsActive, isActive);
            // Fetch the account from the database
            var account = await _accounts.Find(filter).FirstOrDefaultAsync();
            if (account == null)
            {
                // Handle the case where the account doesn't exist
                return new OperationResult<Account>
                {
                    Success = false,
                    Data = null
                };
            }

            await _accounts.UpdateOneAsync(filter, update);
            account.IsActive = isActive;

            // If the account is found, return it with a success status
            return new OperationResult<Account>
            {
                Success = true,
                Data = account
            };
        }
        catch (Exception ex)
        {
            // Handle any other exceptions that occur during the database operation
            return new OperationResult<Account>
            {
                Success = false,
                Data = null
            };
        }
    }
    
    // public async Task<OperationResult<object>> UpdateAccount(string id)
    // {
    //     try
    //     {
    //         // Build a filter to find the account by id
    //         var filter = Builders<Account>.Filter.Eq(a => a.Id, id);
    //
    //         // Fetch the account from the database
    //         var account = await _accounts.Find(filter).FirstOrDefaultAsync();
    //         if (account == null)
    //         {
    //             // Handle the case where the account doesn't exist
    //             return new OperationResult<object>
    //             {
    //                 Success = false,
    //                 Data = null
    //             };
    //         }
    //
    //         // If the account is found, return it with a success status
    //         return new OperationResult<object>
    //         {
    //             Success = true,
    //             Data = account
    //         };
    //     }
    //     catch (Exception ex)
    //     {
    //         // Handle any other exceptions that occur during the database operation
    //         return new OperationResult<object>
    //         {
    //             Success = false,
    //             Data = null
    //         };
    //     }
    // }
}