using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;


namespace TAGBackEndWebApi.Controllers
{
    public class TAGUserController : ApiController
    {
                HttpClient client = new HttpClient();

        //Get:api/TAGUser  Returns all users with populated data from https://reqres.in 

        public async Task<TAGUserResultModel> Get()
        {
            int pageLimit = 3;
            int pageIndex = 1;
            bool isLastPage = false;
            TAGUserResultModel userList = new TAGUserResultModel();
            userList.data = new List<TAGUserModel>();

            //for (int pageIndex = 1; pageIndex <= 5; pageIndex++)
            while (!isLastPage)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://reqres.in");
                HttpResponseMessage response = await client.GetAsync($"api/users?page={pageIndex}&per_page={pageLimit}");
                response.EnsureSuccessStatusCode();
                string userJsonString = await response.Content.ReadAsStringAsync();
                TAGUserResultModel deserialized = JsonConvert.DeserializeObject<TAGUserResultModel>(userJsonString);
                if (deserialized.data.Count < 3)
                    isLastPage = true;

                foreach (TAGUserModel de in deserialized.data)
                {
                    userList.data.Add(de);
                }
                userList.page = deserialized.page;
                userList.per_page = deserialized.per_page;
                userList.total = deserialized.total;
                userList.total_pages = deserialized.total_pages;
                pageIndex++;
            }


            return userList;
        }

        // GET: api/TAGUser?pageIndex= {PageIndex}&pageLimit={pageLimit}  - this end point retrieves data from https://reqres.in based on pageIndex and pageLimit
        // Integer parameters provided by the front-end user 

        public async Task<TAGUserResultModel> GetTAGUserAsync(int pageIndex, int pageLimit)
        {
            client.BaseAddress = new Uri("https://reqres.in");
            HttpResponseMessage response = await client.GetAsync($"api/users?page={pageIndex}&per_page={pageLimit}");
            response.EnsureSuccessStatusCode();
            string userJsonString = await response.Content.ReadAsStringAsync();
            TAGUserResultModel deserialized = JsonConvert.DeserializeObject<TAGUserResultModel>(userJsonString);

            return deserialized;
        }


        // Post: Creat a user and store the info in Json format to the data repo at https://reqres.in    

        public async Task<Uri> CreateTAGUserAsync (TAGUserResultModel userAdded)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/users", userAdded);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        //Delete: Delete a user of the given id from the https://reqres.in data repo
        public async Task<HttpStatusCode> DeleteTAGUserAsync(string id)
        {                // this api end point is set to public only for demo purpose for now
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/users/{id}");
            return response.StatusCode;
        }
    }

}

