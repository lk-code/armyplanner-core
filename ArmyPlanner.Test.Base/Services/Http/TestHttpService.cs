using ArmyPlanner.Interfaces;
using ArmyPlanner.Services.Http;

namespace ArmyPlanner.Test.Base.Services.Http
{
    public class TestHttpService : IHttpService
    {
        #region properties

        private readonly Dictionary<string, object> _registeredContent;

        #endregion

        #region constructors

        public TestHttpService()
        {
            this._registeredContent = new Dictionary<string, object>();
        }

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
            await Task.CompletedTask;

            return (string)this._registeredContent[requestUri];
        }

        /// <summary>
        /// adds a demo content request
        /// </summary>
        /// <param name="requestUri">the request-uri</param>
        /// <param name="content">the content</param>
        public void RegisterHttpContent(string requestUri, object content)
        {
            if (this._registeredContent.ContainsKey(requestUri))
            {
                this._registeredContent.Remove(requestUri);
            }

            this._registeredContent.Add(requestUri, content);
        }

        #endregion
    }
}