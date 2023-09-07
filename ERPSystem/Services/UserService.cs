using ERPSystem.Data;
using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services
{
    public class UserService : IUserService
    {
        private readonly DataBaseContext _dbContext;

        public UserService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public Models.User GetUserById(int id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        public User GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
