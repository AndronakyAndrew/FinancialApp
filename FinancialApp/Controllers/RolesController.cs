using FinancialApp.Model;
using FinancialApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RolesService roleserv;

        public RolesController(RolesService rolesService)
        {
            roleserv = rolesService;
        }

        //HTTP-метод получения всех ролей
        [HttpGet("/roles/all")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await roleserv.GetAllRoles();

            if(roles == null) return NotFound();

            return Ok(roles);
        }

        //HTTP-метод получения ролей по ID
        [HttpGet("/roles/{id?}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await roleserv.GetRoleById(id);

            if(role == null) return NotFound();

            return Ok(role);
        }

        //HTTP-метод для добавления новой роли
        [HttpPost("/roles/create")]
        public async Task<IActionResult> AddRole([FromBody] Role role)
        {
            var result = await roleserv.AddRole(role);
            return Ok(result);
        }
    }
}
