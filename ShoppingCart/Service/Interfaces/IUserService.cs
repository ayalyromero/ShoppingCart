using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Service.Interfaces
{
    public interface IUserService
    {
        User ValidateUser(User model);
    }
}
