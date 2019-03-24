﻿using Newtonsoft.Json;
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

        // GET: api/TAGUser - this end point retrieves data from https://reqres.in based on pageIndex and pageLimit
        //Integer parameters provided by the front-end user 

        public async Task<TAGUserResultModel> GetTAGUserAsync(int pageIndex, int pageLimit)
        {
            client.BaseAddress = new Uri("https://reqres.in");
            HttpResponseMessage response = await client.GetAsync($"api/user?page={pageIndex}&per_page={pageLimit}");
            response.EnsureSuccessStatusCode();
            string userJsonString = await response.Content.ReadAsStringAsync();
            TAGUserResultModel deserialized = JsonConvert.DeserializeObject<TAGUserResultModel>(userJsonString);

            return deserialized;
        }

        // To be modified to return all users  

        public async Task<TAGUserResultModel> GetTAGUserAsync() 
        {
            client.BaseAddress = new Uri("https://reqres.in");
            HttpResponseMessage response = await client.GetAsync("api/users");
            response.EnsureSuccessStatusCode();
            string usersJsonString = await response.Content.ReadAsStringAsync();
            TAGUserResultModel deserialized = JsonConvert.DeserializeObject<TAGUserResultModel>(usersJsonString);
            return deserialized;
        }

        // To be modified to return all users  

        public async Task<Uri> CreateTAGUserAsync (TAGUserResultModel userAdded)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/users", userAdded);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

          public async Task<HttpStatusCode> DeleteTAGUserAsync(string id)
        {                // this api end point is set to public only for demo purpose for now
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/users/{id}");
            return response.StatusCode;
        }
    }

}

