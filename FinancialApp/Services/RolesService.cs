using FinancialApp.Model;
using FinancialApp.Repositories;

namespace FinancialApp.Services
{
    public class RolesService
    {
        private readonly RolesRepository rep;

        public RolesService(RolesRepository repository)
        {
            rep = repository;
        }

        //Метод получения всех ролей
        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await rep.GetAllRoles();
        }

        //Метод поучения роли по ID
        public async Task<Role>GetRoleById(int id)
        {
            return await rep.GetRoleById(id);
        }
        
        //Метод добавления новой роли
        public async Task<string> AddRole(Role role)
        {
            return await rep.AddRole(role);
        }
    }
}
