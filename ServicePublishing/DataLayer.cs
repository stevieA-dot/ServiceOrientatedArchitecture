using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

using Authenticator;

using Newtonsoft.Json;

using RestSharp;

using static APIClasses.Registry;

namespace ServicePublishing
{
    internal class DataLayer
    {
        private static NetTcpBinding tcp = new NetTcpBinding();
        private static string url = "net.tcp://localhost:8100/Authenticator";
        private static ChannelFactory<IAuthenticator> authServerFactory = new ChannelFactory<IAuthenticator>(tcp, url);
        private static IAuthenticator authServer = authServerFactory.CreateChannel();
        private static RestClient _client = new RestClient("https://localhost:44358/");
        public static bool Register(string username, string password)
        {
            return authServer.Register(username, password) == "successfully registered";
        }

        public static int Login(string username, string password)
        {
            return authServer.Login(username, password);
        }

        public async static Task<RestResponse> Publish(int token, EndpointData endpointData)
        {
            RestRequest req = new RestRequest($"registry/publish/{token}");
            req.AddJsonBody(JsonConvert.SerializeObject(endpointData));
            return await _client.PostAsync(req);
        }

        public async static Task<RestResponse> Unpublish(int token, SearchData searchData)
        {
            try
            {
                RestRequest req = new RestRequest($"registry/unpublish/{token}/{searchData}");
                return await _client.DeleteAsync(req);
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
    }
}
