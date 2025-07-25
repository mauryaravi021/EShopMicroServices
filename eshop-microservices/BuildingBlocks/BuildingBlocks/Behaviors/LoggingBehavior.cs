﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            logger.LogInformation("[START] Handle request={Request} - Response={Response} - RequestData={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();
            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3) // if the request is greater than 3 seconds, then log the warnings
                logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds.",
                    typeof(TRequest).Name, timeTaken.Seconds);

            logger.LogInformation("[END] Handle request={Request} - Response={Response} - ResponseData={ResponseData}", typeof(TRequest).Name, typeof(TResponse).Name, JsonSerializer.Serialize(response));
            return response;
        }
    }
}
