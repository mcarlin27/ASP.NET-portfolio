using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class Repository
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public int StargazersCount { get; set; }

        public static List<Repository> GetRepositories()
        {
            var client = new RestClient("https://api.github.com");
            var request = new RestRequest("/users/mcarlin27/repos.json", Method.GET);
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            var repositoryList = JsonConvert.DeserializeObject<List<Repository>>(jsonResponse["repos"].ToString());
            return repositoryList;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
