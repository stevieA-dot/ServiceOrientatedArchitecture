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

using APIClasses;

using Authenticator;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            try
            {
                token = DataLayer.Login(username, password);
            }
            catch(Exception e)
            {
                throw e;
            }
            return token;
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

            }
            else
            {
                throw new Exception(resp.ErrorMessage);
            }

            return tokenExpired;
        }

        public async static Task<bool> Unpublish(int token, SearchData search)
        {
            bool unpublished = false;
            RestResponse resp = null;

            resp = await DataLayer.Unpublish(token, search);
            if (resp != null)
            {
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    unpublished = JsonConvert.DeserializeObject<bool>(resp.Content);
                }
                else
                {
                    throw new Exception(resp.ErrorMessage);
                }
            }

            return unpublished;
        }

        public static bool IsDataNull (EndpointData endpoint)
        {
            if (string.IsNullOrEmpty(endpoint.Name)
                || string.IsNullOrEmpty(endpoint.Description)
                || string.IsNullOrEmpty(endpoint.APIEndpoint)
                || string.IsNullOrEmpty(endpoint.OperandType))
            {
                return true;
            }
            return false;
        }

        public async static Task<bool> ValidateAPIEndpoint(int token, EndpointData endpoint)
        {
            Numbers numbers = new Numbers();
            RestResponse resp = null;
            numbers.NumOne = 1;
            numbers.NumTwo = 2;

            if (endpoint.NumOfOperands == 3)
            {
                numbers.NumThree = 3;
            }

            try
            {
                RestClient client = new RestClient(endpoint.APIEndpoint);
                RestRequest req = new RestRequest($"/{token}");
                req.AddJsonBody(JsonConvert.SerializeObject(numbers));
                resp = await client.PostAsync(req);
            }
            catch(Exception e)
            {
                throw e;
            }

            return resp.IsSuccessful;
        }
    }
}
