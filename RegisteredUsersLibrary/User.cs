using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace RegisteredUsers
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
