using System;

namespace Application.Models
{
    public class ProfileModel
    {
        public string Login { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}