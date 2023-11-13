using fruitful.bll.Repositories;
using fruitful.dal.Models;
using Microsoft.AspNetCore.Mvc;

namespace fruitful.api.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController: ControllerBase
{
    private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAccounts()
    {
        try
        {
            var result = await _accountRepository.GetAccounts();
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
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccountById(string id)
    {
        try
        {
            var result = await _accountRepository.GetAccountById(id);
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

    [HttpPut("active/{id}")]
    public async Task<IActionResult> UpdateAccountActive(string id, [FromQuery] bool isActive)
    {
        try
        {
            var result = await _accountRepository.UpdateAccountActive(id, isActive);
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