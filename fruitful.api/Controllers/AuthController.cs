using fruitful.bll.Repositories;
using Fruitful.BLL.Services;
using fruitful.dal.Collections;
using fruitful.dal.Models;
using Microsoft.AspNetCore.Mvc;

namespace fruitful.api.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;
    private readonly JWTService _jwtService;

    public AuthController(IAccountRepository accountRepository, JWTService jwtService)
    {
        _accountRepository = accountRepository;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationBody body)
    {
        try
        {
            var result = await _accountRepository.Register(body);
            return Ok(result);
        }
        catch (Exception e)
        {
            if (e is DuplicateWaitObjectException)
            {
                return Conflict(new OperationResult<object>()
                {
                    Success = false,
                    Data = null
                });
            }

            return StatusCode(500, new OperationResult<object>()
            {
                Success = false,
                Data = null
            });
        }
    }
}