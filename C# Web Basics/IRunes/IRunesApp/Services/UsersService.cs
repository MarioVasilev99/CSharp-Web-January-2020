using IRunesApp.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IRunesApp.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool EmailExists(string email)
        {
            return this.db.Users.Any(u => u.Email == email);
        }

        public string GetUserId(string username, string password)
        {
            var hashedPassword = this.Hash(password);
            var user = this.db.Users
                .FirstOrDefault(
                    u => u.Username == username &&
                    u.Password == hashedPassword);

            if (user == null)
            {
                return null;
            }

            return user.Id;
        }

        public string GetUsername(string id)
        {
            return this.db.Users
                .Where(u => u.Id == id)
                .Select(u => u.Username)
                .FirstOrDefault();
        }

        public void Register(string username, string password, string email)
        {
            var user = new User
            {
                Username = username,
                Password = this.Hash(password),
                Email = email
            };
            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public bool UsernameExists(string username)
        {
            return this.db.Users.Any(u => u.Username == username);
        }

        private string Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
