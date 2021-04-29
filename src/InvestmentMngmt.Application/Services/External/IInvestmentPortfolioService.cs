using InvestmentMngmt.Domain.External.Dtos;
using Refit;
using System.Threading.Tasks;

namespace InvestmentMngmt.Application.Services.External
{
    public interface IInvestmentPortfolioService
    {
        [Get("/{key}")]
        Task<TreasuryDirectDto> GetTreasuryDirectsAsync(string key);

        [Get("/{key}")]
        Task<FixedIncomeDto> GetFixedIncomesAsync(string key);

        [Get("/{key}")]
        Task<FundDto> GetFundsAsync(string key);
    }
}