using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RestSharp;

using static APIClasses.Registry;
using static APIClasses.ServiceProvider;

namespace GUI
{
    public partial class Menu : System.Web.UI.Page
    {
        private static int token;
        private static RestClient _client;
        public static string PanelHeader = "";
        private static string apiEndpoint = "";
        private static int result = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //ad labels and cols to aspx
            // make buttons clickable
            if (Request["token"] != null)
            {
                token = Int32.Parse(Request["token"].ToString());
            }
            
            if (!Page.IsPostBack)
            {
                apiEndpoint = Request["apiEndpoint"] != null ? Request["apiEndpoint"] : null;

            }
            
        }

        protected async void allServicesBtn_Click(object sender, EventArgs e)
        {
            // display all services and make them clickable
            // display all services is done through the registry api
            // check token first
            // create request
            // ping the registry api
            try
            {
                litResults.Text = "";
                _client = new RestClient("https://localhost:44358/");
                RestRequest req = new RestRequest($"registry/allServices/{token}");
                RestResponse resp = await _client.GetAsync(req);

                if (resp.IsSuccessful && !string.IsNullOrEmpty(resp.Content))
                {
                    List<EndpointData> endpoints = JsonConvert.DeserializeObject<List<EndpointData>>(resp.Content);

                        // loop through the endpoints list and create a link
                        // send the information in the url

                    foreach (var endpoint in endpoints)
                    {
                        var openLink = String.Format("<a href=\"javascript:ExceuteAPICall({0},'{1}','{2}');\" class='btn btn-default btn-xs'> Test</a>", endpoint.NumOfOperands, endpoint.Name, endpoint.APIEndpoint);

                        litResults.Text += String.Format("<tr>\n" +
                                                            "<td>{0}</td><td>{1}</td><td>{2}</td>\n" +
                                                            "</tr>\n\n",
                                                            endpoint.Name,
                                                            endpoint.Description,
                                                            openLink);
                    }
                }
            }
            catch(Exception e1)
            {
                MessageBox.Show($"An error occured: {e1.Message}");
                Response.Redirect("/Login");
            }
        }

        protected async void searchBtn_Click(object sender, EventArgs e)
        {
            SearchData search = new SearchData();
            search.SearchStr = searchTxt.Text;

            try
            {
                litResults.Text = "";
                // build api request
                _client = new RestClient("https://localhost:44358/");
                RestRequest req = new RestRequest($"registry/search/{token}");
                req.AddJsonBody(JsonConvert.SerializeObject(search));
                RestResponse resp = await _client.PostAsync(req);

                // deserialise data table
                // check for status code
                // check to see if data table contains entries
                // loop through entries
                // create link to open them in the panel
                if (resp.IsSuccessful && !string.IsNullOrEmpty(resp.Content))
                {
                    List<EndpointData> endpoints = JsonConvert.DeserializeObject<List<EndpointData>>(resp.Content);

                    foreach (var endpoint in endpoints)
                    {
                        var openLink = String.Format("<a href=\"javascript:ExceuteAPICall({0},'{1}','{2}'); \" class='btn btn-default btn-xs'> Test</a>", endpoint.NumOfOperands, endpoint.Name, endpoint.APIEndpoint);

                        litResults.Text += String.Format("<tr>\n" +
                                                            "<td>{0}</td><td>{1}</td><td>{2}</td>\n" +
                                                            "</tr<\n\n",
                                                            endpoint.Name,
                                                            endpoint.Description,
                                                            openLink);
                    }
                }
            }
            catch(Exception e1)
            {
                MessageBox.Show($"An error occured: {e1.Message}");
                Response.Redirect("/Login");
            }
        }

        protected async void testBtn_Click(object sender, EventArgs e)
        {
            // get the values of the text boxes
            // if third is empty then we're only sending to two
            // use api endpoint to send the values accross

            int numOne, numTwo, numThree;

            numOne = !string.IsNullOrEmpty(numOnetxt.Text) ? Int32.Parse(numOnetxt.Text) : 0;
            numTwo = !string.IsNullOrEmpty(numTwotxt.Text) ? Int32.Parse(numTwotxt.Text) : 0;
            numThree = !string.IsNullOrEmpty(numThreetxt.Text) ? Int32.Parse(numThreetxt.Text) : 0;


            // build request with apiendpoint
            _client = new RestClient(apiEndpoint);
            RestRequest req = new RestRequest($"/{token}");
            Numbers numbers = new Numbers();

            if (string.IsNullOrEmpty(numThreetxt.Text))
            {
                
                numbers.NumOne = numOne;
                numbers.NumTwo = numTwo;
            }
            else
            {
                numbers.NumThree = numThree;
            }
            req.AddJsonBody(JsonConvert.SerializeObject(numbers));

            try
            {
                RestResponse resp = await _client.PostAsync(req);
                if (resp.IsSuccessful)
                {
                    result = JsonConvert.DeserializeObject<int>(resp.Content);
                    resultsTxt.Text = result.ToString();
                }

            }
            catch(Exception e1)
            {
                MessageBox.Show($"An error occured: {e1.Message}");
                Response.Redirect("/Login");
            }
        }
    }
}