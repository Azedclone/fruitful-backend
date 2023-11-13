using fruitful.dal.Collections;
using fruitful.dal.Models;

namespace fruitful.bll.Repositories;

public interface IAccountRepository
{
    Task<OperationResult<List<Account>>> GetAccounts();
    Task<OperationResult<Account>> GetAccountById(string id);
    // Task<OperationResult<object>> UpdateAccount(string id, Account account);
    Task<OperationResult<Account>> UpdateAccountActive(string id, bool isActive);
    
    Task<OperationResult<Account>> Register(RegistrationBody body);
    Task<OperationResult<Account>> Login(LoginBody body);
}