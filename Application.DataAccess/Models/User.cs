using System;
using Application.DataAccess.Exceptions;

namespace Application.DataAccess.Models
{
    public sealed class User
    {

        private User()
        {
        }

        public User(string login, string password)
        {
            this.Login    = !string.IsNullOrWhiteSpace(login) ? login : throw new IncorrectLoginException("Login must be provided");
            this.Password = !string.IsNullOrWhiteSpace(password) ? password : throw new IncorrectPasswordException("Password must be provided");
        }

        public long UserId { get; private set; }

        public string Login { get; private set; }

        public string Password { get; private set; }

        public string Surname { get; private set; }

        public string Name { get; private set; }

        public string LastName { get; private set; }

        public DateTime Birthday { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public void UpdateFullName(string name, string surname, string lastName)
        {
            this.Name     = name;
            this.Surname  = surname;
            this.LastName = lastName;
        }

        public void UpdateBirthday(DateTime birthday)
        {
            this.Birthday = birthday;
        }

        public void UpdatePhone(string phone)
        {
            this.Phone = phone;
        }

        public void UpdateEmail(string email)
        {
            this.Email = email;
        }
    }
}