using fruitful.dal.Collections;
using fruitful.dal.Models;

namespace fruitful.bll.Repositories;

public interface IProductRepository
{
    Task<OperationResult<List<Product>>> GetProducts();
    Task<OperationResult<Product>> AddProduct(AddProductBody body);
}