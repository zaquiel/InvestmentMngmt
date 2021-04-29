using FluentAssertions;
using InvestmentMngmt.Application.Investment.Commands.Get;
using InvestmentMngmt.Application.Services;
using InvestmentMngmt.Application.Services.External;
using InvestmentMngmt.Domain.External;
using InvestmentMngmt.Domain.External.Dtos;
using InvestmentMngmt.Domain.Settings;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace InvestmentMngmt.Tests.Application.Services
{
    public class InvestmentSummaryServiceTest: BaseTest
    {
        private readonly Mock<IInvestmentPortfolioService> _investmentPortfolioService;
        private readonly IOptions<ExternalServicesSettings> _externalServicesSettings;
        private readonly InvestmentSummaryService _investmentSummaryService;

        public InvestmentSummaryServiceTest()
        {
            _investmentPortfolioService = new Mock<IInvestmentPortfolioService>();
            _externalServicesSettings = Options.Create(ExternalServicesSettings);
            _investmentSummaryService = new InvestmentSummaryService(_investmentPortfolioService.Object, _externalServicesSettings);
        }

        [Fact]
        public async Task ShouldBeCorrectResult()
        {
            var expectedTreasuryDirectDto = new TreasuryDirectDto() { TreasuryDirects = new List<TreasuryDirect>() };
            var expectedFixedIncomesDto = new FixedIncomeDto() { FixedIncomes = new List<FixedIncome>() };
            var expectedFundsDto = new FundDto() { Funds = new List<Fund>() };

            _investmentPortfolioService.Setup(x => x.GetTreasuryDirectsAsync(_externalServicesSettings.Value.Uris.TreasuryDirect)).ReturnsAsync(expectedTreasuryDirectDto);
            _investmentPortfolioService.Setup(x => x.GetFixedIncomesAsync(_externalServicesSettings.Value.Uris.FixedIncome)).ReturnsAsync(expectedFixedIncomesDto);
            _investmentPortfolioService.Setup(x => x.GetFundsAsync(_externalServicesSettings.Value.Uris.Funds)).ReturnsAsync(expectedFundsDto);

            var result = await _investmentSummaryService.GetSummaryAsync();

            result.Should().BeOfType<Result>();

            result.TotalValue.Should().Equals(expectedTreasuryDirectDto.TreasuryDirects.Sum(x => x.TotalValue)
                + expectedFixedIncomesDto.FixedIncomes.Sum(x => x.TotalValue) + expectedFundsDto.Funds.Sum(x => x.TotalValue));

            result.Investments.Should().NotBeNull();
        }
    }
}
