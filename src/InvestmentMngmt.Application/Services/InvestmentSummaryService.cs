using InvestmentMngmt.Application.Investment.Commands.Get;
using InvestmentMngmt.Application.Services.External;
using InvestmentMngmt.Domain.External;
using InvestmentMngmt.Domain.Settings;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace InvestmentMngmt.Application.Services
{
    public class InvestmentSummaryService: IInvestmentSummaryService
    {
        private readonly IInvestmentPortfolioService _investmentPortfolioService;
        private readonly IOptions<ExternalServicesSettings> _options;

        public InvestmentSummaryService(IInvestmentPortfolioService investmentPortfolioService, IOptions<ExternalServicesSettings> options)
        {
            _investmentPortfolioService = investmentPortfolioService;
            _options = options;
        }

        public async Task<Result> GetSummaryAsync()
        {
            var treasuryDirects = await _investmentPortfolioService.GetTreasuryDirectsAsync(_options.Value?.Uris?.TreasuryDirect);
            var fixedIncomes = await _investmentPortfolioService.GetFixedIncomesAsync(_options.Value?.Uris?.FixedIncome);
            var funds = await _investmentPortfolioService.GetFundsAsync(_options.Value?.Uris?.Funds);

            var result = (treasuryDirects.TreasuryDirects.Cast<InvestmentBase>()
                .Concat(fixedIncomes.FixedIncomes.Cast<InvestmentBase>())
                .Concat(funds.Funds.Cast<InvestmentBase>())).Select(x => (InvestmentMngmt.Domain.Investment)x);

            return new Result(result);
        }
    }
}
