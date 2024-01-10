using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twitter.Business.Dtos.AuthDtos;
using Twitter.Business.Services.Interfaces;

namespace Twitter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        IAuthService _authService { get; }

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                return Ok(await  _authService.Login(dto));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
