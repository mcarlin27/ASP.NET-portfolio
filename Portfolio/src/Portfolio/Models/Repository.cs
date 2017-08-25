using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class Repository
    {
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public int stargazers_count { get; set; }

        public static List<Repository> GetRepositories()
        {
            RestClient client = new RestClient("https://api.github.com");
            RestRequest request = new RestRequest("/search/repositories?q=user:mcarlin27&sort=stars&per_page=3", Method.GET);
            RestResponse response = new RestResponse();
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
