using fruitful.dal.Collections;
using fruitful.dal.Models;
using MongoDB.Driver;

namespace fruitful.bll.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IMongoCollection<Category> _categories;

    public CategoryRepository(IMongoDatabase db)
    {
        _categories = db.GetCollection<Category>(nameof(Category));
    }

    public async Task<OperationResult<List<Category>>> GetCategories()
    {
        try
        {
            // Fetch all products from the database
            var productList = await _categories.Find(_ => true).ToListAsync();
            // Console.WriteLine(accountsList[0]);

            // If the list is not empty, return it with a success status
            return new OperationResult<List<Category>>
            {
                Success = true,
                Data = productList
            };
        }
        catch (Exception ex)
        {
            return new OperationResult<List<Category>>
            {
                Success = false,
                Data = null,
            };
        }
    }

    public async Task<OperationResult<Category>> AddCategory(string name)
    {
        try
        {
            var category = new Category
            {
                Name = name
            };

            await _categories.InsertOneAsync(category);
            return new OperationResult<Category>
            {
                Success = true,
                Data = category
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new OperationResult<Category>
            {
                Success = false,
                Data = null
            };
        }
    }

    public async Task<OperationResult<object>> ChangeEnableStatus(string id, bool isEnable)
    {
        try
        {
            var filter = Builders<Category>.Filter.Eq(a => a.Id, id);
            var update = Builders<Category>.Update.Set(a => a.IsEnable, isEnable);

            var category = await _categories.Find(filter).FirstOrDefaultAsync();
            if (category == null)
            {
                return new OperationResult<object>
                {
                    Success = false,
                    Data = null
                };
            }

            await _categories.UpdateOneAsync(filter, update);
            category.IsEnable = isEnable;

            return new OperationResult<object>
            {
                Success = true,
                Data = category
            };
        }
        catch (Exception ex)
        {
            // Handle any other exceptions that occur during the database operation
            return new OperationResult<object>
            {
                Success = false,
                Data = null
            };
        }
    }
}