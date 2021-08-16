using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Application.Models
{
    public sealed class LoginRequest
    {
        [BindProperty]
        [Required, MinLength(3)]
        public string Login { get; set; }

        [BindProperty]
        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}