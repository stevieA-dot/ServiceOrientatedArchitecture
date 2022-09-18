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
        private static string _usersFilePath = AppDomain.CurrentDomain.BaseDirectory + "users.txt";
        private static string _tokensFilePath = AppDomain.CurrentDomain.BaseDirectory + "tokens.txt";
        public static User ReadUserFromFile(string username, string password)
        {
            User foundUser = null;

            if (File.Exists(_usersFilePath))
            {
                using (var reader = new StreamReader(_usersFilePath))
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
            if (!File.Exists(_usersFilePath))
            {
                using (var writer = new StreamWriter(_usersFilePath))
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
                using (var writer = new StreamWriter(_usersFilePath, true))
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

            if (File.Exists(_tokensFilePath))
            {
                using (var reader = new StreamReader(_tokensFilePath))
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

            if (!File.Exists(_tokensFilePath))
            {
                using (var writer = new StreamWriter(_tokensFilePath))
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
                using (var writer = new StreamWriter(_tokensFilePath, true))
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
            if (File.Exists(_tokensFilePath))
            {
                File.Delete(_tokensFilePath);
            }
        }
    }
}
