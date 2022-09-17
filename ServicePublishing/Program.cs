using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Newtonsoft.Json.Linq;

using static APIClasses.Registry;

namespace ServicePublishing
{
    internal class Program
    {
        private static int token = 0;
        static async Task Main(string[] args)
        {

            LoginOrRegister();

            string option = null;
            while (option != "e")
            {
                Console.WriteLine("Would you like to (p)ublish or (u)npublish a service or (e)xit?");
                option = Console.ReadLine();

                switch (option)
                {
                    case "p":
                        EndpointData endpoint = null;
                        while (endpoint == null || !ValidateEndpoint(endpoint).Result)
                        {
                            endpoint = PublishGUI();
                        }
                        if (await Publish(endpoint))
                        {
                            Console.WriteLine("Token has expired. Please login again.");
                            LoginGui();
                        }
                        else
                        {
                            Console.WriteLine($"{endpoint} has been published");
                        }
                        break;
                    case "u":
                        SearchData searchData = UnpublishGUI();

                        try
                        {
                            if (!await Unpublish(searchData))
                            {
                                Console.WriteLine("Search term wasn't found and the endpoint was not deleted.");
                            }
                            else
                            {
                                Console.WriteLine($"{searchData} has been unpublished");
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);

                        }
                        
                        break;
                        default:
                        Console.WriteLine($"Option {option} does not exist");
                        break;
                }
            }
        }

        private static void LoginOrRegister()
        {
            string loginOrRegister = "";

            while (loginOrRegister != "l" && loginOrRegister != "r")
            {
                Console.WriteLine("Would you like to (l)ogin or (r)egister?");
                {
                    loginOrRegister = Console.ReadLine();

                    switch (loginOrRegister)
                    {
                        case "l":
                            LoginGui();
                            break;
                        case "r":
                            RegisterGUI();
                            break;
                        default:
                            Console.WriteLine("Please enter l for login or r for register");
                            break;
                    }
                }
            }
        }
        private static EndpointData PublishGUI()
        {
            EndpointData endpoint = new EndpointData();
            while (BusinessLayer.IsDataNull(endpoint))
            {
                Console.WriteLine("Lets publish a service! Please enter the name of the service");
                endpoint.Name = Console.ReadLine();

                Console.WriteLine("Please enter the description");
                endpoint.Description = Console.ReadLine();

                Console.WriteLine("Please enter the API endpoint");
                endpoint.APIEndpoint = Console.ReadLine();

                while (!endpoint.OperandType.ToLower().Contains("int"))
                {
                    Console.WriteLine("Please enter the type of operands. You may select (int)eger");
                    endpoint.OperandType = Console.ReadLine();
                }

                while (endpoint.NumOfOperands > 3 || endpoint.NumOfOperands < 2)
                {
                    Console.WriteLine("Enter number of operands. Options are 2 or 3. Must be a whole number.");
                    Int32.TryParse(Console.ReadLine(), out endpoint.NumOfOperands);
                }
            }
            Console.WriteLine($"Your endpoint is {endpoint}");

            return endpoint;
        }

        private async static Task<bool> ValidateEndpoint(EndpointData endpoint)
        {
            bool validEndpoint = false;
            try
            {
                if (!await BusinessLayer.ValidateAPIEndpoint(token, endpoint))
                {
                    Console.WriteLine("Endpoint does not exist, please try again");
                    PublishGUI();
                }
                else
                {
                    Console.WriteLine("Endpoint was valid. Thank you");
                    validEndpoint = true;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"There was an error with your API endpoint : {e.Message}");
            }

            return validEndpoint;
        }
        private async static Task<bool> Publish(EndpointData endpoint)
        {
            return await BusinessLayer.Publish(token, endpoint);
        }

        private static SearchData UnpublishGUI()
        {
            SearchData searchData = new SearchData();
            while (string.IsNullOrEmpty(searchData.SearchStr))
            {
                Console.WriteLine("Lets unpublish a service. Please enter the API endpoint");
                searchData.SearchStr = Console.ReadLine();
            }
            return searchData;
        }

        private async static Task<bool> Unpublish(SearchData searchData)
        {
            return await BusinessLayer.Unpublish(token, searchData);
        }

        private static void RegisterGUI()
        {
            string username = null, password = null;
            do
            {
                Console.WriteLine("Welcome, please register by entering your username followed by enter and password followed by enter");
                username = Console.ReadLine();
                password = Console.ReadLine();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("You must enter a username and password");
                }
            }
            while (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password));
            if (BusinessLayer.Register(username, password))
            {
                Console.WriteLine("You have successfully registered");
            }
            else
            {
                Console.WriteLine("That username and password already exist. Please try again");
                RegisterGUI();
            }
            
        }

        private static void LoginGui()
        {
            string username = null, password = null;
            while (token == 0)
            {
                Console.WriteLine("Lets login! Please enter your username followed by enter then your password followed by enter");
                username = Console.ReadLine();
                password = Console.ReadLine();
                token = BusinessLayer.Login(username, password);
            }
            Console.WriteLine("You have successfully logged in");
        }
    }
}
