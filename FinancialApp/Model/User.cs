using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FinancialApp.Model
{
    public class User : IdentityUser<int> { }
}
