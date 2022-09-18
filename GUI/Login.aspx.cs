using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

using Authenticator;

using RestSharp;

namespace GUI
{
    public partial class Contact : Page
    {
        private static string username;
        private static string password;

        private static NetTcpBinding tcp = new NetTcpBinding();
        private static string url = "net.tcp://localhost:8100/Authenticator";
        private static ChannelFactory<IAuthenticator> authServerFactory = new ChannelFactory<IAuthenticator>(tcp, url);
        private static IAuthenticator authServer = authServerFactory.CreateChannel();
        private static int token;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void registerBtn_Click(object sender, EventArgs e)
        {
            username = usernameTxt.Text;
            password = paswordTxt.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and password must be entered");
            }
            else
            {
                //call authenticator
                if (authServer.Register(username, password) != "successfully registered")
                {
                    errorLbl.Text= "Username already exists. Please try again";
                }
                else
                {
                    errorLbl.Text = "Successfully registered";
                }
            }
        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            username = usernameTxt.Text;
            password = paswordTxt.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                errorLbl.Text = "Username already exists. Please try again";
            }
            else
            {
                //call authenticator
                token = authServer.Login(username, password);
                if (token == 0)
                {
                    errorLbl.Text = "Error loggin in. Please try again";
                }
                else
                {
                    //response redirect to menu page
                    Response.Redirect($"/Menu?token={token}");
                }
            }
        }
    }
}