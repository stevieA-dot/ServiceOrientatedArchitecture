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
            bool valid = false;

            try
            {
                NetTcpBinding tcp = new NetTcpBinding();
                string url = "net.tcp://localhost:8100/Authenticator";
                ChannelFactory<IAuthenticator> authServerFactory = new ChannelFactory<IAuthenticator>(tcp, url);
                IAuthenticator authServer = authServerFactory.CreateChannel();

                valid = authServer.Validate(token);
            }
            catch(Exception e)
            {
                throw e;
            }

            return valid;
        }
        public static void WriteEndPointToFile(EndpointData data)
        {
            try
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
            catch(Exception e)
            {
                throw e;
            }
        }

        public static List<EndpointData> ReturnAllServices()
        {
            List<EndpointData> endpoints = new List<EndpointData>();
            try
            {
                if (File.Exists(_fileName))
                {
                    using (var reader = new StreamReader(_fileName))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Context.RegisterClassMap<EndpointDataMap>();
                        endpoints = csv.GetRecords<EndpointData>().ToList();
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }

            return endpoints;
        }

        public static bool RemoveEndPoint(EndpointData endpoint)
        {
            var endpoints = ReturnAllServices();
            EndpointData foundEndpoint = null;

            try
            {
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
            }
            catch(Exception e)
            {
                throw e;
            }

            return foundEndpoint != null;
        }

        public static List<EndpointData> FindEndpointWithSearchTerm(SearchData data)
        {
            try
            {
                var endpoints = ReturnAllServices();
                List<EndpointData> result = new List<EndpointData>();

                if (endpoints.Count != 0)
                {
                    result = endpoints.Where(e => e.APIEndpoint.ToLower().Contains(data.SearchStr.ToLower())).ToList();
                }
            }
            catch(Exception e)
            {
                throw e;
            }

            return result;
        }


    }
}