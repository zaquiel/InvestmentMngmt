using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestmentMngmt.API.Presentation
{
    /// <summary>
    /// 
    /// </summary>
    public struct HttpResponseError
    {
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> Errors { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errors"></param>
        public HttpResponseError(string message, IReadOnlyCollection<Notification> errors)
        {
            this.Message = message;
            this.Errors = errors.Select(notification => notification.Message);
        }
    }
}
