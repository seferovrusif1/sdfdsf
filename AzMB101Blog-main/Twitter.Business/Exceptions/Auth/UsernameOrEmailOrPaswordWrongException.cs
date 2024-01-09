using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Business.Exceptions.Auth
{
    public class UsernameOrEmailOrPaswordWrongException : Exception
    {
        public UsernameOrEmailOrPaswordWrongException():base("Username or Password is wrong!!"){        }
        public UsernameOrEmailOrPaswordWrongException(string? message) : base(message){}
    }
}
