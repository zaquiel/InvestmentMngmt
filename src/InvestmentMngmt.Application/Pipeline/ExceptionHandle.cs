using Flunt.Notifications;
using InvestmentMngmt.Application.Investment.Commands.Get;
using MediatR.Pipeline;
using OpenTracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InvestmentMngmt.Application.Pipeline
{
    public class ExceptionHandle: IRequestExceptionHandler<Request, Response<Result>>
    {
        private readonly ITracer _tracer;

        public ExceptionHandle(ITracer tracer)
        {
            _tracer = tracer;
        }

        public Task Handle(Request request, Exception exception, RequestExceptionHandlerState<Response<Result>> requestExceptionHandlerState, CancellationToken cancellationToken)
        {            
            var response = new Response<Result>("Error getting Investment Summary", new Notification(exception.Source, $"{Error().Message}: {exception.Message}"), Error().StatusCode);
            requestExceptionHandlerState.SetHandled(response);

            _tracer.ActiveSpan.SetTag("error", true);
            _tracer.ActiveSpan.SetTag("statusCode", (int)Error().StatusCode);

            return Task.CompletedTask;

            (string Message, HttpStatusCode StatusCode) Error()
                => exception.Source == "Refit" ? ("Error on External API", requestExceptionHandlerState.Response?.StatusCode ?? HttpStatusCode.BadRequest) : (exception.GetType().Name, HttpStatusCode.InternalServerError);
        }
    }
}
