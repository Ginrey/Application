using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Application.Models
{
    public sealed class RegisterRequest
    {
        [BindProperty]
        [Required, MinLength(3)]
        public string Login { get; set; }

        [BindProperty]
        [Required, MinLength(6)]
        public string Password { get; set; }

        [BindProperty]
        [DisplayName("Confirm password")]
        [Required, Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }

        [BindProperty]
        [Required, MinLength(2)]
        public string Surname { get; set; }

        [BindProperty]
        [Required, MinLength(2)]
        public string Name { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Birthday { get; set; }
        
        [BindProperty]
        [Required, RegularExpression("^\\A[^@]+@[^@\\.]+\\.+[^@\\.]+\\z$", ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        
        [BindProperty]
        [Required, RegularExpression(@"^((\+7|7|8)+([0-9]){10})$", ErrorMessage = "Invalid phone")]
        public string Phone { get; set; }
        
    }
}