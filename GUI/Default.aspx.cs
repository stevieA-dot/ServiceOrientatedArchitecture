using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Authenticator;

namespace GUI
{
    public partial class _Default : Page
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

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            username = usernameTxt.Text;
            password = passwordTxt.Text;

           

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

        protected void registerBtn_Click(object sender, EventArgs e)
        {
            username = usernameTxt.Text;
            password = passwordTxt.Text;

            //call authenticator
            string registration = authServer.Register(username, password);
            //call authenticator
            if (registration != "successfully registered")
            {
                errorLbl.Text = "Username already exists. Please try again";
            }
            else
            {
                errorLbl.Text = "Successfully registered";
            }
        }
    }
}