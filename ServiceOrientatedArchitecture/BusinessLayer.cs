using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CsvHelper;

using static ServiceOrientatedArchitecture.DataLayer;

namespace Authenticator
{
    internal class BusinessLayer
    {
        public static User ReadUserFromFile(string username, string password)
        {
            User foundUser = null;

            if (File.Exists("users.txt"))
            {
                using (var reader = new StreamReader("users.txt"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<UserMap>();
                    var users = csv.GetRecords<User>().ToList();

                    foundUser = users.Where(u => u.username.ToLower() == username.ToLower() && u.password == password).FirstOrDefault();
                }
            }

            return foundUser;
        }

        public static void WriteUserToFile(User user)
        {
            if (!File.Exists("users.txt"))
            {
                using (var writer = new StreamWriter("users.txt"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<UserMap>();
                    csv.WriteHeader<User>();
                    csv.NextRecord();
                    csv.WriteRecord(user);
                }
            }
            else
            {
                using (var writer = new StreamWriter("users.txt", true))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<UserMap>();
                    csv.NextRecord();
                    csv.WriteRecord(user);
                }
            }
        }

        public static int ReadTokenFromFile(int token)
        {
            Token foundToken = null;

            if (File.Exists("tokens.txt"))
            {
                using (var reader = new StreamReader("tokens.txt"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<TokenMap>();
                    var tokens = csv.GetRecords<Token>().ToList();

                    foundToken = tokens.Where(t => t.token == token).FirstOrDefault();
                }
            }
            return foundToken == null ? 0 : foundToken.token;
        }

        public static void WriteTokenToFile(int token)
        {
            Token newToken = new Token();
            newToken.token = token;

            if (!File.Exists("tokens.txt"))
            {
                using (var writer = new StreamWriter("tokens.txt"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<TokenMap>();
                    csv.WriteHeader<Token>();
                    csv.NextRecord();
                    csv.WriteRecord(newToken);
                }
            }
            else
            {
                using (var writer = new StreamWriter("tokens.txt", true))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<TokenMap>();
                    csv.NextRecord();
                    csv.WriteRecord(newToken);
                }
            }
        }

        public static void ClearTokens()
        {
            if (File.Exists("tokens.txt"))
            {
                File.Delete("tokens.txt");
            }
        }
    }
}
