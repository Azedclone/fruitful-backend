using fruitful.dal.Collections;
using fruitful.dal.Models;

namespace fruitful.bll.Repositories;

public interface IAccountRepository
{
    Task<OperationResult<Account>> Register(RegistrationBody body);
}