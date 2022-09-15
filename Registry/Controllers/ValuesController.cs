using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using static APIClasses.Registry;

namespace Registry.Controllers
{
    [RoutePrefix("registry")]
    public class ValuesController : ApiController
    {
        private const string _status = "Denied";
        private const string _reason = "Authentication Error";

        [Route("publish/{token}/{endpointData}")]
        public DataTable Publish(int token, [FromBody] EndpointData endpointData)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Status");
            dt.Columns.Add("Reason");

            if (BusinessLayer.Authenticate(token))
            {
                BusinessLayer.WriteEndPointToFile(endpointData);
            }
            else
            {
                dt.Rows.Add(_status, _reason);
            }

            return dt;
        }

        [Route("search/{token}/{searchData}")]
        public DataTable Search(int token, [FromBody] SearchData searchData)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Status");
            dt.Columns.Add("Reason");

            if (BusinessLayer.Authenticate(token))
            {
                List<EndpointData> endpoints = BusinessLayer.FindEndpointWithSearchTerm(searchData);

                if (endpoints.Count() != 0)
                {
                    dt.Columns.Add("Name");
                    dt.Columns.Add("Description");
                    dt.Columns.Add("APIEndpoint");
                    dt.Columns.Add("NumOfOperands");
                    dt.Columns.Add("OperandType");

                    foreach (var endpoint in endpoints)
                    {
                        dt.Rows.Add(
                            endpoint.Name,
                            endpoint.Description,
                            endpoint.APIEndpoint,
                            endpoint.NumOfOperands,
                            endpoint.OperandType);
                    }
                }
                
            }
            else
            {
                dt.Rows.Add(_status, _reason);
            }

            return dt;
        }

        [Route("allServices/{token}")]
        public DataTable AllServices(int token)
        {
            DataTable dt = new DataTable();

            if (BusinessLayer.Authenticate(token))
            {
                List<EndpointData> endpoints = BusinessLayer.ReturnAllServices();

                if (endpoints.Count() != 0)
                {
                    dt.Columns.Add("Name");
                    dt.Columns.Add("Description");
                    dt.Columns.Add("APIEndpoint");
                    dt.Columns.Add("NumOfOperands");
                    dt.Columns.Add("OperandType");

                    foreach (var endpoint in endpoints)
                    {
                        dt.Rows.Add(
                            endpoint.Name,
                            endpoint.Description,
                            endpoint.APIEndpoint,
                            endpoint.NumOfOperands,
                            endpoint.OperandType);
                    }
                }
                
            }
            else
            {
                dt.Columns.Add("Status");
                dt.Columns.Add("Reason");
                dt.Rows.Add(_status, _reason);
            }

            return dt;
        }

        [Route("unpublish/{token}/{endpoint}")]
        public DataTable Unpublish(int token, [FromBody] EndpointData endpoint)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Status");
            dt.Columns.Add("Reason");

            if (BusinessLayer.Authenticate(token))
            {
                BusinessLayer.RemoveEndPoint(endpoint);
            }
            else
            {
                dt.Rows.Add(_status, _reason);
            }

            return dt;
        }
    }
}
