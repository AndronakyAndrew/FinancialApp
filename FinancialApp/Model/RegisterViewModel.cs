﻿using System.ComponentModel.DataAnnotations;

namespace FinancialApp.Model
{
    public class RegisterViewModel
    {

        [Required]
        [EmailAddress]
        public string Email {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
