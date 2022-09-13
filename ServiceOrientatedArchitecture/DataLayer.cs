using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CsvHelper.Configuration;

namespace ServiceOrientatedArchitecture
{
    internal class DataLayer
    {
        public class User
        {
            public string username;
            public string password;

            public User()
            {
                username = default;
                password = default;
            }
        }

        public sealed class UserMap : ClassMap<User>
        {
            public UserMap()
            {
                Map(m => m.username).Index(0).Name("username");
                Map(m => m.password).Index(1).Name("password");
            }
        }

        public class Token
        {
            public int token;

            public Token()
            {
                token = 0;
            }
        }

        public sealed class TokenMap : ClassMap<Token>
        {
            public TokenMap()
            {
                Map(m => m.token);
            }
        }
    }
}
