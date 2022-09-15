using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;

using Authenticator;

using CsvHelper;

using static APIClasses.Registry;

namespace Registry
{
    public class BusinessLayer
    {
        private const string _fileName = "apiEndpoints.txt";

        public static bool Authenticate(int token)
        {
            NetTcpBinding tcp = new NetTcpBinding();
            string url = "net.tcp://localhost:8100/Authenticator";
            ChannelFactory<IAuthenticator> authServerFactory = new ChannelFactory<IAuthenticator>(tcp, url);
            IAuthenticator authServer = authServerFactory.CreateChannel();

            return authServer.Validate(token);
        }
        public static void WriteEndPointToFile(EndpointData data)
        {
            if (!File.Exists(_fileName))
            {
                using (var writer = new StreamWriter(_fileName))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<EndpointDataMap>();
                    csv.WriteHeader<EndpointData>();
                    csv.NextRecord();
                    csv.WriteRecord(data);
                }
            }
            else
            {
                using (var writer = new StreamWriter(_fileName, true))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<EndpointDataMap>();
                    csv.NextRecord();
                    csv.WriteRecord(data);
                }
            }
        }

        public static List<EndpointData> ReturnAllServices()
        {
            List<EndpointData> endpoints = new List<EndpointData>();
            if (File.Exists(_fileName))
            {
                using (var reader = new StreamReader(_fileName))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<EndpointDataMap>();
                    endpoints = csv.GetRecords<EndpointData>().ToList();
                }
            }

            return endpoints;
        }

        public static bool RemoveEndPoint(EndpointData endpoint)
        {
            var endpoints = ReturnAllServices();
            EndpointData foundEndpoint = null;

            if (endpoints.Count != 0)
            {
                foundEndpoint = endpoints.Where(e => e.Name.ToLower() == endpoint.Name.ToLower()).FirstOrDefault();

                if (foundEndpoint != null)
                {
                    endpoints.Remove(foundEndpoint);

                    using (var writer = new StreamWriter(_fileName))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.Context.RegisterClassMap<EndpointDataMap>();
                        csv.WriteHeader<EndpointData>();
                        csv.NextRecord();
                        csv.WriteRecords(endpoints);
                    }
                }
            }
            return foundEndpoint != null;
        }

        public static List<EndpointData> FindEndpointWithSearchTerm(SearchData data)
        {
            var endpoints = ReturnAllServices();
            List<EndpointData> result = new List<EndpointData>();

            if (endpoints.Count != 0)
            {
                result = endpoints.Where(e => e.APIEndpoint.Contains(data.SearchStr)).ToList();
            }

            return result;
        }


    }
}