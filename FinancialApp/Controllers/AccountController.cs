using FinancialApp.Data;
using FinancialApp.Model;
using FinancialApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService accountService;

        public AccountController(AccountService _accountService)
        {
            accountService = _accountService;
        }

        //Метод для получения всех счетов
        [HttpGet("/accounts/all")]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await accountService.GetAccount();

            if(accounts == null)
                return NotFound();
            
            return Ok(accounts);
        }

        //Метод для получения счета и баланса
        [HttpGet("/account")]
        public async Task<IActionResult> GetAccountByID(int id)
        {
            var account = await accountService.GetAccountById(id);

            if(account == null) 
                return NotFound();
           
            return Ok(account);
        }

        //Метод для добавленния счета
        [HttpPost("/account/create")]
        public async Task<IActionResult> CreateAcount([FromBody] Account account)
        {
            if (account == null)
                return BadRequest("Счет с такими данными уже существует");

            var result = await accountService.CreateAccount(account);
            return Ok(result);
        }

        //Метод для пополнения счета
        [HttpPost("/account/deposit")]
        public async Task<IActionResult> DepositBalance([FromBody] Account account, int id, double sum)
        {

            if (account == null || sum <= 0)
                return BadRequest("Некорректные данные для пополнения счета");

            var result = await accountService.Deposit(id, sum);
            return Ok(result);

        }

        //Метод для снятия средств
        [HttpPost("/account/withdraw")]
        public async Task<IActionResult> WithdrawBalance([FromBody] Account account, int id, int sum)
        {
            if (sum <= 0 || account == null)
                return BadRequest("Некоректные данные для снятия средств");
           
            var result = await accountService.WithDraw(id, sum);
            return Ok(result);
        }
    }
}
