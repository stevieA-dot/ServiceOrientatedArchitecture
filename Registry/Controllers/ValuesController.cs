using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Web.Http;

using APIClasses;

using Newtonsoft.Json.Linq;

using static APIClasses.Registry;

namespace Registry.Controllers
{
    [RoutePrefix("registry")]
    public class ValuesController : ApiController
    {
        private ServerStatus serverStat = new ServerStatus();

        [Route("publish/{token}/{endpointData}")]
        public void Publish(int token, [FromBody] EndpointData endpointData)
        {
            try
            {
                if (BusinessLayer.Authenticate(token))
                {
                    BusinessLayer.WriteEndPointToFile(endpointData);
                }
                else
                {
                    Request.CreateResponse(HttpStatusCode.Unauthorized, serverStat);
                }
            }
            catch(Exception e)
            {
                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [Route("search/{token}/{searchData}")]
        public List<EndpointData> Search(int token, [FromBody] SearchData searchData)
        {
            List<EndpointData> endpoints = new List<EndpointData>();

            try
            {
                if (BusinessLayer.Authenticate(token))
                {
                    endpoints = BusinessLayer.FindEndpointWithSearchTerm(searchData);
                }
                else
                {
                    Request.CreateResponse(HttpStatusCode.Unauthorized, serverStat);
                }
            }
            catch(Exception e)
            {
                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

            return endpoints;
        }

        [Route("allServices/{token}")]
        public List<EndpointData> AllServices(int token)
        {
            List<EndpointData> endpoints = new List<EndpointData>();
            
            if (BusinessLayer.Authenticate(token))
            {
                endpoints = BusinessLayer.ReturnAllServices();
            }
            else
            {
                HttpResponseException exp = new HttpResponseException(Request.CreateResponse(HttpStatusCode.Unauthorized, serverStat));
                throw exp;
            }

            return endpoints;
        }

        [Route("unpublish/{token}/{endpoint}")]
        [HttpDelete]
        public bool Unpublish(int token, [FromBody] EndpointData endpoint)
        {

            if (BusinessLayer.Authenticate(token))
            {
                return BusinessLayer.RemoveEndPoint(endpoint);
            }
            else
            {
                HttpResponseException exp = new HttpResponseException(Request.CreateResponse(HttpStatusCode.Unauthorized, serverStat));
                throw exp;
            }
        }
    }
}
