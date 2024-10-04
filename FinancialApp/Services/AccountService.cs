
using FinancialApp.Model;
using FinancialApp.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace FinancialApp.Services
{
    public class AccountService 
    {
        private readonly AccountRepository accrepository;

        public AccountService(AccountRepository accountRepository)
        {
            accrepository = accountRepository;
        }

        //Метод для получения всех счетов
        public async Task<IEnumerable<Account>> GetAccount()
        {
            return await accrepository.GetAccounts();
        }

        //Метод для создания счета
        public async Task<string> CreateAccount(Account account)
        {
             return await accrepository.CreateAccount(account);
        }

        //Метод для получения счета
        public async Task<Account> GetAccountById(int id)
        {
            return await accrepository.GetAccountById(id);
        }

        //Метод для пополнения счета
        public async Task<string> Deposit(int id, double sum)
        {
            return await accrepository.Deposit(id, sum);
        }

        //Метод для снятия средств
        public async Task<string> WithDraw(int id, int sum)     
        {  
           return await accrepository.Withdraw(id, sum);
        }
    }
}
