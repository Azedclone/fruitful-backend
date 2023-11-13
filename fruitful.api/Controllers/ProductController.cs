using fruitful.bll.Repositories;
using fruitful.dal.Models;
using Microsoft.AspNetCore.Mvc;

namespace fruitful.api.Controllers;


[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        try
        {
            var result = await _productRepository.GetProducts();
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, new OperationResult<object>()
            {
                Success = false,
                Data = null
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] AddProductBody body)
    {
        try
        {
            var result = await _productRepository.AddProduct(body);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, new OperationResult<object>()
            {
                Success = false,
                Data = null
            });
        }
    }
}