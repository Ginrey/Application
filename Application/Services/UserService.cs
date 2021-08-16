using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.DataAccess.Exceptions;
using Application.DataAccess.Models;
using Application.DataAccess.Repository;
using Application.Models;

namespace Application.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task RegisterUserAsync(RegisterRequest model)
        {
            var user = await this.userRepository.FindByLoginAsync(model.Login.ToLower());

            if (user != null)
            {
                throw new UserAlreadyRegisteredException("User with this login already registered");
            }

            var newUser = new User(model.Login.ToLower(), GetHashPasswordBase64(model.Password));

            newUser.UpdateFullName(model.Name, model.Surname, model.LastName);
            newUser.UpdateBirthday(model.Birthday);
            newUser.UpdatePhone(model.Phone);
            newUser.UpdateEmail(model.Email);

            await this.userRepository.Save(newUser);
        }

        public async Task<ProfileModel> AuthUserAsync(string login, string password)
        {
            var user           = await this.userRepository.FindByLoginAsync(login);
            var passwordBase64 = GetHashPasswordBase64(password);

            if (user == null || user.Password != passwordBase64)
            {
                throw new NotFoundUserException("Login or password invalid");
            }

            return new ProfileModel
            {
                Login    = user.Login,
                Name     = user.Name,
                Surname  = user.Surname,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Email    = user.Email,
                Phone    = user.Phone,
            };
        }

        private static string GetHashPasswordBase64(string password)
        {
            var hashPassword   = MD5.HashData(Encoding.UTF8.GetBytes(password));
            var base64Password = Convert.ToBase64String(hashPassword);

            return base64Password;
        }
    }
}