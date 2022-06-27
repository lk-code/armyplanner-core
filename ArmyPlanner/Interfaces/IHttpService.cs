﻿using System.Threading.Tasks;

namespace ArmyPlanner.Interfaces
{
    public interface IHttpService
    {
        /// <summary>
        /// Send a GET request to the specified Uri and return the response body as a string#
        /// in an asynchronous operation.
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<string> GetStringAsync(string requestUri);
    }
}