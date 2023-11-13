using fruitful.dal.Collections;
using fruitful.dal.Models;

namespace fruitful.bll.Repositories;

public interface ICategoryRepository
{
    Task<OperationResult<List<Category>>> GetCategories();
    Task<OperationResult<Category>> AddCategory(string name);
    Task<OperationResult<object>> ChangeEnableStatus(string id, bool isEnable);
}