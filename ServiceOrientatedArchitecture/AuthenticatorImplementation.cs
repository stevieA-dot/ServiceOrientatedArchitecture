using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using static ServiceOrientatedArchitecture.DataLayer;

namespace Authenticator
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, UseSynchronizationContext = false, InstanceContextMode = InstanceContextMode.Single)]
    public class AuthenticatorImp : IAuthenticator
    {
        private static AuthenticatorImp instance = null;

        public static AuthenticatorImp GetInstance()
        {
            if (instance == null)
            {
                return new AuthenticatorImp();
            }
            else
            {
                return instance;
            }
        }
        private AuthenticatorImp(){ }

        public int Login(string username, string password)
        {
            // checks the DB to see if the login exists
            Random rand = new Random();
            int token = 0;
            User user = BusinessLayer.ReadUserFromFile(username, password);

            if (user == null)
            {
                //alert user that login does not exist
                MessageBox.Show($"Login {username}, {password} doesn't exist. Please register first");
            }
            else
            {
                int foundToken = 0;

                // generate a token that doesn't exist
                while (foundToken == token)
                {
                    token = rand.Next(1, 1000);

                    foundToken = BusinessLayer.ReadTokenFromFile(token).token;
                }

                BusinessLayer.WriteTokenToFile(token);
            }
            return token;
        }

        public string Register(string username, string password)
        {
            // adds registered user into the db
            User user = BusinessLayer.ReadUserFromFile(username, password);
            string succesful = null;

            if (user == null)
            {
                user = new User();
                user.username = username;
                user.password = password;
                BusinessLayer.WriteUserToFile(user);
                succesful = "successfully registered";
            }
            else
            {
                succesful = "unsuccessful registration";
            }
            return succesful;
        }

        public bool Validate(int token)
        {
            //checks with the db to see if the token is valid
            return BusinessLayer.ReadTokenFromFile(token) != null;
        }

        public void ClearTokens()
        {
            BusinessLayer.ClearTokens();
        }
    }
}
