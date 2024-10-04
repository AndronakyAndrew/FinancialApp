using System.ComponentModel.DataAnnotations;

namespace FinancialApp.Model
{
    public class Account
    {
        [Required]
        public int Id { get; set; }
        public int AccountNumber {  get; set; }
        public double Balance { get; set; }
        public int UserId { get; set; }
    }
}
