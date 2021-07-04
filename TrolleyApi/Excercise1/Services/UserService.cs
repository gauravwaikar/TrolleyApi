using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrolleyApi.Excercise1
{
    public interface IUserService
    {
        public UserResponse Get();
    }

    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserResponse Get()
        {
            return new UserResponse("Gaurav Waikar", _configuration["UserToken"]);
        }
    }
}
