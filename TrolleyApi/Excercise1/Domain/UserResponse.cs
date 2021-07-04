using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrolleyApi.Excercise1
{
    public class UserResponse
    {
        public string Name { get; }
        public string Token { get; }

        public UserResponse(string name, string token)
        {
            Name = name;
            Token = token;
        }
    }
}
