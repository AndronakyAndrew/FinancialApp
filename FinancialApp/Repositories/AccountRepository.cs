using FinancialApp.Data;
using FinancialApp.Model;
using Microsoft.EntityFrameworkCore;

namespace FinancialApp.Repositories
{
    public class AccountRepository
    {
        private readonly FinancialAppContext db;

        public AccountRepository(FinancialAppContext context)
        {
            db = context;
        }

        //Метод для получения всех счетов
        public async Task<IEnumerable<Account>> GetAccounts()
        {
            return await db.Accounts.ToListAsync();
        }

        //Метод для получения счета
        public async Task<Account> GetAccountById(int id)
        {
            return await db.Accounts.FindAsync(id);
        }

        //Метод для создания счета
        public async Task<string> CreateAccount(Account account)
        {
            var acc = db.Accounts.FirstOrDefault(a => a.Id == account.Id);

            if(acc != null) return "Такой счет уже существует";
            
            await db.Accounts.AddAsync(account);
            await db.SaveChangesAsync();
            return "Счет успешно добавлен";
        }

        //Метод для пополнения счета
        public async Task<string> Deposit(int id, double sum)
        {
            var acc = await db.Accounts.FirstOrDefaultAsync(a => a.Id == id);

            //Проверка на наличие счета для пополнения
            if(acc == null) return "Счет не найден";
            
            //Проверка чтобы вносимая сумма была положительная
            if(sum <= 0)  return "Вносимая сумма не должна быть отрицательной";

            //Процесс пополнения
            acc.Balance += sum;
            await db.SaveChangesAsync();
            return $"Пополнение успешно. Баланс: {acc.Balance}";
        }

        //Метод для снятия средств со счета
        public async Task<string> Withdraw(int id, int sum)
        {
            var acc = await db.Accounts.FirstOrDefaultAsync(a => a.Id == id);

            if(acc == null) return "Счет не найден";
           
            else if(sum > acc.Balance) return $"Недостаточно средств для проведения операции. Баланс: {acc.Balance}";
            
            acc.Balance -= sum;
            await db.SaveChangesAsync();
            return $"Операция прошла успешно. Баланс: {acc.Balance}";
        }
    }
}
