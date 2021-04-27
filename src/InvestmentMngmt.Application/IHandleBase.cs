using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentMngmt.Application
{
    public interface IHandlerBase<T, K> : IRequestHandler<T, K> where T : IRequest<K>
    {
    }
}
