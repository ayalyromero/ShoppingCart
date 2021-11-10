using ShoppingCart.Models;
using ShoppingCart.Persistence;
using ShoppingCart.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Service.Implementations
{
    public class UserService : IUserService
    {

        private readonly ApiContext _context;

        public UserService(ApiContext context)
        {
            _context = context;
        }

        public User ValidateUser(User model)
        {
            var user =  _context.Users.Where(_ => _.Username == model.Username && _.Password == model.Password).FirstOrDefault();

            if (user == null)
                return model;
            else
                return user;
        }
    }
}
