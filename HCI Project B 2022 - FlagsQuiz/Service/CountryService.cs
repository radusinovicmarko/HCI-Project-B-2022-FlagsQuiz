using HCI_Project_B_2022___FlagsQuiz.Data.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project_B_2022___FlagsQuiz.Service
{
    internal class CountryService
    {
        private static readonly string endpoint = "https://restcountries.com/v2/all?fields=name,region,flag";
        public List<Country> GetAll()
        {
            RestClient client = new RestClient(endpoint);
            var response = client.GetAsync(new RestRequest());
            return JsonConvert.DeserializeObject<List<Country>>(response.Result.Content);
        }
    }
}
