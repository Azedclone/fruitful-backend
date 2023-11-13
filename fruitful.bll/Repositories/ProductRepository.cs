using fruitful.dal.Collections;
using fruitful.dal.Models;
using MongoDB.Driver;

namespace fruitful.bll.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _products;

    public ProductRepository(IMongoDatabase db)
    {
        _products = db.GetCollection<Product>(nameof(Product));
    }


    public async Task<OperationResult<List<Product>>> GetProducts()
    {
        try
        {
            // Fetch all products from the database
            var productList = await _products.Find(_ => true).ToListAsync();
            // Console.WriteLine(accountsList[0]);

            // If the list is not empty, return it with a success status
            return new OperationResult<List<Product>>
            {
                Success = true,
                Data = productList
            };
        }
        catch (Exception ex)
        {
            return new OperationResult<List<Product>>
            {
                Success = false,
                Data = null,
            };
        }
    }

    public async Task<OperationResult<Product>> AddProduct(AddProductBody body)
    {
        try
        {
            var product = new Product
            {
                Name = body.Name,
                CostPrice = body.CostPrice,
                SellPrice = body.SellPrice,
                QuantityInStock = body.QuantityInStock,
                Image = body.Image,
                CategoryId = body.CategoryId,
                ExpiredAt = body.ExpiredAt
            };

            await _products.InsertOneAsync(product);
            return new OperationResult<Product>
            {
                Success = true,
                Data = product
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new OperationResult<Product>
            {
                Success = false,
                Data = null
            };
        }
    }
}