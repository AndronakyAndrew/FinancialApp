using FinancialApp.Data;
using FinancialApp.Model;
using Microsoft.EntityFrameworkCore;

namespace FinancialApp.Repositories
{
    public class RolesRepository
    {
        private readonly FinancialAppContext db;

        public RolesRepository(FinancialAppContext context)
        {
            db = context;
        }

        //Метод для получения всех ролей
        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await db.Roles.ToListAsync();
        }

        //Метод для получения роли по ID
        public async Task<Role> GetRoleById(int id)
        {
            return await db.Roles.FindAsync(id);
        }

        //Метод для добавления роли
        public async Task<string> AddRole(Role role)
        {
            var roles = await db.Roles.FirstOrDefaultAsync(r => r.Id == role.Id);

            if (roles != null) return "Такая роль уже существует";

            await db.Roles.AddAsync(role);
            await db.SaveChangesAsync();
            return "Добавление успешно";
        }

    }
}
