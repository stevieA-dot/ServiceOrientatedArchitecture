using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

using Authenticator;

using Newtonsoft.Json;

using RestSharp;

using static APIClasses.Registry;
using static APIClasses.ServiceProvider;

namespace ServicePublishing
{
    internal class BusinessLayer
    {
        public static bool Register(string username, string password)
        {
            // checks for null values first
            // if null values are found returns false
            // attempts to register user given the username and password
            // if username already exists
            // then register attempt does not go through

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            
            return DataLayer.Register(username, password);
        }

        public static int Login(string username, string password)
        {
            // checks if either are null
            // if they are sends back 0
            // attempts to login user given the username and password
            // if username does not exist
            // will return a token of zero

            int token = 0;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return token;
            }

            return DataLayer.Login(username, password);
        }

        public static async Task<bool> Publish(int token, EndpointData endpoint)
        {

            RestResponse resp = await DataLayer.Publish(token, endpoint);
            DataTable dt = null;
            bool tokenExpired = false;

            if (resp.IsSuccessful)
            {
                // deserialize data
                // get whether token is valid
                dt = JsonConvert.DeserializeObject<DataTable>(resp.Content);
                tokenExpired = dt.AsDataView().Find("Status").ToString() == "Denied";
            }
            else
            {
                throw new Exception(resp.ErrorMessage);
            }

            return tokenExpired;
        }

        public async static Task<bool> Unpublish(int token, SearchData search)
        {
            // given the name of a service
            // will attempt to delete that service
            // if it is not successful
            // will return false

            DataTable dt = null;
            bool tokenExpired = false;

            RestResponse resp = await DataLayer.Unpublish(token, search);
            if (resp.IsSuccessful)
            {
                dt = JsonConvert.DeserializeObject<DataTable>(resp.Content);
                tokenExpired = dt.AsDataView().Find("Status").ToString() == "Denied";
            }
            else
            {
                throw new Exception(resp.ErrorMessage);
            }

            return tokenExpired;
        }

        public static bool CheckForNullData (EndpointData endpoint)
        {
            if (string.IsNullOrEmpty(endpoint.Name)
                || string.IsNullOrEmpty(endpoint.Description)
                || string.IsNullOrEmpty(endpoint.APIEndpoint)
                || string.IsNullOrEmpty(endpoint.OperandType))
            {
                return false;
            }
            return true;
        }

        public async static Task<bool> ValidateAPIEndpoint(int token, EndpointData endpoint)
        {
            ThreeNums numbers = new ThreeNums();
            numbers.NumOne = 1;
            numbers.NumTwo = 2;

            if (endpoint.NumOfOperands == 3)
            {
                numbers.NumThree = 3;
            }

            RestClient client = new RestClient(endpoint.APIEndpoint);
            RestRequest req = new RestRequest($"/{token}");
            req.AddJsonBody(JsonConvert.SerializeObject(numbers));
            RestResponse resp = await client.PostAsync(req);

            return resp.IsSuccessful;
        }
    }
}
