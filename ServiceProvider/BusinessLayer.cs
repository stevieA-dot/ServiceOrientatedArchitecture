using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

using Authenticator;

namespace ServiceProvider
{
    public class BusinessLayer
    {
        public static bool Authenticate(int token)
        {
            NetTcpBinding tcp = new NetTcpBinding();
            string url = "net.tcp://localhost:8100/Authenticator";
            ChannelFactory<IAuthenticator> authServerFactory = new ChannelFactory<IAuthenticator>(tcp, url);
            IAuthenticator authServer = authServerFactory.CreateChannel();
            
            return authServer.Validate(token);
        }
    }
}