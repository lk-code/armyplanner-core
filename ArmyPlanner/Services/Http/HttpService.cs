using ArmyPlanner.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArmyPlanner.Services.Http
{
    public class HttpService : IHttpService
    {
        #region properties

        #endregion

        #region constructors

        #endregion

        #region logic

        /// <summary>
        /// Send a GET request to the specified Uri and return the response body as a string#
        /// in an asynchronous operation.
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<string> GetStringAsync(string requestUri)
        {
            HttpClient httpClient = this.CreateHttpClient();

            return await httpClient.GetStringAsync(requestUri);
        }

        /// <summary>
        /// creates an http-client instance
        /// </summary>
        /// <returns>the generated http-client instance</returns>
        private HttpClient CreateHttpClient()
        {
            HttpClient httpClient = new HttpClient();

            return httpClient;
        }

        #endregion
    }
}