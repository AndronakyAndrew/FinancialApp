using FinancialApp.Data;
using FinancialApp.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace FinancialApp.Repositories
{
    public class UserRepository
    {
        private readonly IConfiguration config;
        private readonly SignInManager<User> signInManage;
        private readonly UserManager<User> userManage;
        private readonly FinancialAppContext db;

        public UserRepository(UserManager<User> userManager, FinancialAppContext context, SignInManager<User> signInManager, IConfiguration configuration)
        {
            config = configuration;
            signInManage = signInManager;
            userManage = userManager;
            db = context;
        }  

        //Мето для получения всех пользователей
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await db.Users.ToListAsync();
        }

        //Метод для получения польщователя по email
        public async Task<User> GetUserByEmail(string email)
        {
            return await userManage.FindByEmailAsync(email);
        }

        //Метод для проверкки пароля
        public async Task<bool> CheckPassword(User user, string password)
        {
            return await userManage.CheckPasswordAsync(user, password);
        }

        //Метод для генерации JWT-токена
        public async Task<string> GenerateJwtToken(User user)
        {
            var securityToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config["Jwt:key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = securityToken.CreateToken(tokenDescriptor);
            return securityToken.WriteToken(token);
        }

        //Метод для регистрации пользователя и добавления в базу данных
        public async Task<IdentityResult> Register(User user, string password)
        {
            return await userManage.CreateAsync(user, password);
        }

    }
}
