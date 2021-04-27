using InvestmentMngmt.Application.Investment.Commands.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentMngmt.Application.Services
{
    public interface IInvestmentSummaryService
    {
        Task<Result> GetSummaryAsync();
    }
}
