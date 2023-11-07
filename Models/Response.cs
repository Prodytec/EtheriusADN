#pragma warning disable

using System.Linq.Expressions;

namespace EtheriusWebAPI.Models
{
    public class Response
    {
        /// <summary>
        /// returns true if successful, otherwise false.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// returns Success or Error Message according to condition.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Returns the data.
        /// </summary>
        public dynamic Data { get; set; }    


    }
}
