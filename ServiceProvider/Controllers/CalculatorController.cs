using System.Data;
using System.Web.Http;

using static APIClasses.ServiceProvider;

namespace ServiceProvider.Controllers
{
    [RoutePrefix("api/Calculator")]
    public class CalculatorController : ApiController
    {
        private const string _status = "Denied";
        private const string _reason = "Authentication Error";

        [Route("addTwoNumbers/{token}/{numbers}")]
        public DataTable AddTwoNumbers(int token, [FromBody] TwoNums numbers)
        {
            DataTable dt = new DataTable();

            if (BusinessLayer.Authenticate(token))
            {
                int result = numbers.NumOne + numbers.NumTwo;
                dt.Columns.Add("Result");
                dt.Rows.Add(result);
            }
            else
            {
                dt.Columns.Add("Status");
                dt.Columns.Add("Reason");
                dt.Rows.Add(_status, _reason);
            }
            return dt;
        }

        [Route("addThreeNumbers/{token}/{numbers}")]
        public DataTable AddThreeNumbers( int token, [FromBody] ThreeNums numbers)
        {
            DataTable dt = new DataTable();

            if (BusinessLayer.Authenticate(token))
            {
                int result = numbers.NumOne + numbers.NumTwo + numbers.NumThree;
                dt.Columns.Add("Result");
                dt.Rows.Add(result);
            }
            else
            {
                dt.Columns.Add("Status");
                dt.Columns.Add("Reason");
                dt.Rows.Add(_status, _reason);
            }
            return dt;
        }

        [Route("mulTwoNumbers/{token}/{numbers}")]
        public DataTable MulTwoNumbers(int token, [FromBody] TwoNums numbers)
        {
            DataTable dt = new DataTable();

            if (BusinessLayer.Authenticate(token))
            {
                int result = numbers.NumOne * numbers.NumTwo;
                dt.Columns.Add("Result");
                dt.Rows.Add(result);
            }
            else
            {
                dt.Columns.Add("Status");
                dt.Columns.Add("Reason");
                dt.Rows.Add(_status, _reason);
            }
            return dt;
        }

        [Route("mulThreeNumbers/{token}/{numbers")]
        public DataTable MulThreeNumnbers(int token, [FromBody] ThreeNums numbers)
        {
            DataTable dt = new DataTable();

            if (BusinessLayer.Authenticate(token))
            {
                int result = numbers.NumOne * numbers.NumTwo * numbers.NumThree;
                dt.Columns.Add("Result");
                dt.Rows.Add(result);
            }
            else
            {
                dt.Columns.Add("Status");
                dt.Columns.Add("Reason");
                dt.Rows.Add(_status, _reason);
            }
            return dt;
        }


    }
}
