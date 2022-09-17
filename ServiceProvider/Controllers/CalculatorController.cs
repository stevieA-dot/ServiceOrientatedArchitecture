using System.Data;
using System.Net.Http;
using System.Net;
using System.Web.Http;

using APIClasses;

using Newtonsoft.Json.Linq;

using static APIClasses.ServiceProvider;

namespace ServiceProvider.Controllers
{
    [RoutePrefix("calculator")]
    public class CalculatorController : ApiController
    {
        ServerStatus serverStat = new ServerStatus();

        [Route("addTwoNumbers/{token}")]
        public int AddTwoNumbers(int token, [FromBody] Numbers numbers)
        {
            int result = 0;
            if (BusinessLayer.Authenticate(token))
            {
                result = numbers.NumOne + numbers.NumTwo;
            }
            else
            {
                HttpResponseException exp = new HttpResponseException(Request.CreateResponse(HttpStatusCode.Unauthorized, serverStat));
                throw exp;
            }
            
            return result;
        }

        [Route("addThreeNumbers/{token}")]
        public int AddThreeNumbers( int token, [FromBody] Numbers numbers)
        {
            int result = 0;

            if (BusinessLayer.Authenticate(token))
            {
                result = numbers.NumOne + numbers.NumTwo + numbers.NumThree;
            }
            else
            {
                HttpResponseException exp = new HttpResponseException(Request.CreateResponse(HttpStatusCode.Unauthorized, serverStat));
                throw exp;
            }
            return result;
        }

        [Route("mulTwoNumbers/{token}")]
        public int MulTwoNumbers(int token, [FromBody] Numbers numbers)
        {
            int result = 0;

            if (BusinessLayer.Authenticate(token))
            {
                result = numbers.NumOne * numbers.NumTwo;
            }
            else
            {
                HttpResponseException exp = new HttpResponseException(Request.CreateResponse(HttpStatusCode.Unauthorized, serverStat));
                throw exp;
            }
            return result;
        }

        [Route("mulThreeNumbers/{token}")]
        public int MulThreeNumnbers(int token, [FromBody] Numbers numbers)
        {
            int result = 0;

            if (BusinessLayer.Authenticate(token))
            {
                result = numbers.NumOne * numbers.NumTwo * numbers.NumThree;
            }
            else
            {
                HttpResponseException exp = new HttpResponseException(Request.CreateResponse(HttpStatusCode.Unauthorized, serverStat));
                throw exp;
            }
            return result;
        }


    }
}
