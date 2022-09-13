using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    internal class Program
    {
        private static IAuthenticator _authenticator = AuthenticatorImp.GetInstance();
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Authenticator");

            ServiceHost host;
            NetTcpBinding tcp = new NetTcpBinding();
            Uri[] baseAddresses = new Uri[] { new Uri("net.tcp://0.0.0.0:8100/Authenticator") };
            tcp.MaxReceivedMessageSize = 20000000;
            host = new ServiceHost(_authenticator, baseAddresses);
            host.AddServiceEndpoint(typeof(IAuthenticator), tcp, "net.tcp://0.0.0.0:8100/Authenticator");


            host.Open();
            Console.WriteLine("System online");
            Console.ReadLine();
            host.Close();
        }
    }
}
