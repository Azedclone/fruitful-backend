using fruitful.bll.Repositories;
using fruitful.dal.Models;
using Microsoft.AspNetCore.Mvc;

namespace fruitful.api.Controllers;

[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            var result = await _categoryRepository.GetCategories();
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
    public async Task<IActionResult> AddCategory(string name)
    {
        try
        {
            var result = await _categoryRepository.AddCategory(name);
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

    [HttpPut("enable/{id}")]
    public async Task<IActionResult> ChangeEnableStatus(string id, [FromQuery] bool isEnable)
    {
        try
        {
            var result = await _categoryRepository.ChangeEnableStatus(id, isEnable);
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