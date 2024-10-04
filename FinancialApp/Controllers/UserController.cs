using FinancialApp.Model;
using FinancialApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userServ;
        
        public UserController(UserService userService)
        {
            userServ = userService;
        }

        //HTTP-метод для отображения всех клиентов
        [HttpGet("/user/all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userServ.GetAllUsers();

            if (users == null) return NotFound();

            return Ok(users);
        }

        //HTTP-метод для проведения регистрации
        [HttpPost("/user/register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await userServ.Register(model.Email, model.Password);

            if (result.Succeeded)
            {
                return Ok("Регистрация успешна");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        //HTTP-метод для проведения авторизации
        [HttpPost("/users/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var token = await userServ.Login(model.Email, model.Password);
            if(token == null)
            {
                return Unauthorized(new { message = "Введенные email или пароль неверны" });
            }

            return Ok("Авторизация прошла успешно. С возвращением!");
        }
    }
}
