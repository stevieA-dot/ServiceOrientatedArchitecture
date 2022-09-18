using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using APIClasses;

using static APIClasses.Registry;

namespace Registry.Controllers
{
    [RoutePrefix("registry")]
    public class ValuesController : ApiController
    {
        private ServerStatus serverStat = new ServerStatus();

        [Route("publish/{token}")]
        public void Publish(int token, [FromBody] EndpointData endpointData)
        {
            if (BusinessLayer.Authenticate(token))
            {
                BusinessLayer.WriteEndPointToFile(endpointData);
            }
            else
            {
                HttpResponseException exp = new HttpResponseException(Request.CreateResponse(HttpStatusCode.Unauthorized, serverStat));
                throw exp;
            }
        }

        [Route("search/{token}")]
        public List<EndpointData> Search(int token, [FromBody] SearchData searchData)
        {
            List<EndpointData> endpoints = new List<EndpointData>();

            if (BusinessLayer.Authenticate(token))
            {
                endpoints = BusinessLayer.FindEndpointWithSearchTerm(searchData);
            }
            else
            {
                HttpResponseException exp = new HttpResponseException(Request.CreateResponse(HttpStatusCode.Unauthorized, serverStat));
                throw exp;
            }

            return endpoints;
        }

        [Route("allServices/{token}")]
        [HttpGet]
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

        [Route("unpublish/{token}")]
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
