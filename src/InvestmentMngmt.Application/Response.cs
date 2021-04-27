using System;
using Domain = InvestmentMngmt.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Flunt.Notifications;
using System.Net;

namespace InvestmentMngmt.Application
{
    public class Response<TResponse> : Notifiable<Notification>
    {
        public Response(string message, Notification notification, HttpStatusCode statusCode)
        {
            ErrorMessage = message;
            AddNotification(notification);
            StatusCode = statusCode;
        }

        public Response(TResponse data) => Data = data;

        public string ErrorMessage { get; set; }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public TResponse Data { get; init; }
    }
}
