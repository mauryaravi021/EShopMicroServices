using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
        where TResponse : notnull
    {
    }

    public interface IQueryHandler<in TCommand> : IRequestHandler<TCommand, Unit>
        where TCommand : ICommand<Unit>
    {
    }
}
