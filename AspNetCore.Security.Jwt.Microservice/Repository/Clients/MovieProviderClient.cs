using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCore.Security.Jwt.Microservice.Repository.Clients
{
    /// <summary>
    /// Class MovieProviderClient
    /// </summary>
    public class MovieProviderClient : IMovieProviderClient        
    {
        HttpClient _client;
        string _token;
        int _noOfRetries;

        public MovieProviderClient(string token, int noOfRetries)
        {
            _token = token;
            _noOfRetries = noOfRetries; 
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="TResponse">The response type</typeparam>
        /// <param name="url">The url</param>
        /// <returns><see cref="Task{TResponse}"/></returns>
        public async Task<TResponse> Get<TResponse>(string url)
            where TResponse : class
        {            
            int i = 0;
            bool isError = false;
            do
            {
                try
                {
                    isError = false;

                    _client = new HttpClient() { Timeout = new TimeSpan(0, 0, 2) };
                    _client.DefaultRequestHeaders.Add("x-access-token", _token);

                    return await _client.GetAsync(url)
                                        .ContinueWith(async x =>
                                        {
                                            var result = x.Result;

                                            result.EnsureSuccessStatusCode();

                                            var response = await result.Content.ReadAsStringAsync();

                                            return JsonConvert.DeserializeObject<TResponse>(response);
                                        }).Result;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"Attempt {i+1} failed to get data from Provider {url}. {ex.Message}. Trying again.");
                    isError = true;
                }
                i++;
            }
            while (isError && (i < _noOfRetries));

            return null;            
        }
    }
}
