using Goals.API.Abstractions.Services;
using Goals.API.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace Goals.API.Controllers
{
    [ApiController]
    [Route("/api/user/")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("v1/login")]
        public async Task<IActionResult> Login(LoginInputDTO dto)
        {
            var result = await _service.Login(dto);

            return Ok(result);
        }

        [HttpPost("v1/register")]
        public async Task<IActionResult> CreateAsync(RegisterUserInputDTO dto)
        {
            var result = await _service.CreateAsync(dto);

            return StatusCode(201, result);
        }
    }
}
