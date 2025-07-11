using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler<in IQuery, TResponse> : IRequestHandler<IQuery, TResponse>
        where IQuery : IQuery<TResponse>
        where TResponse : notnull
    {
    }
}
