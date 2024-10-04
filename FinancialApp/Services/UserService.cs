using FinancialApp.Model;
using FinancialApp.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApp.Services
{
    public class UserService
    {
        private readonly UserRepository userrep;

        public UserService(UserRepository userRepository)
        {
            userrep = userRepository;
        }

        //Логика метода для отображения всех пользователей
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await userrep.GetAllUsersAsync();
        }

        //Логика метода для регистрации пользоватеей
        public async Task<IdentityResult> Register(string email, string password)
        {
             var existUser = await userrep.GetUserByEmail(email);

            if (existUser != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email уже используется" });
            }
            var user = new User()
            {
                    UserName = email,
                    Email = email
             };
            return await userrep.Register(user, password);
        }

        //Метод для обработки логики входа
        public async Task<string> Login(string email, string password)
        {
            var user = await userrep.GetUserByEmail(email);
            if (user == null || (!await userrep.CheckPassword(user, password)))
            {
                return null; //Email или пароль неверны
            }

            return await userrep.GenerateJwtToken(user);
        }
    }
}
