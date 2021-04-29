using FluentAssertions;
using InvestmentMngmt.Domain;
using InvestmentMngmt.Domain.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InvestmentMngmt.Tests.Domain
{
    public class InvestmentTest: BaseDataTest
    {
        [Theory]
        [MemberData(nameof(DataProfitabilityPositive))]
        public void ShouldBeProcessCorrectlyIRWhenProfitabilityPositive<T>(T entity, decimal irPercent) where T : InvestmentBase
        {
            entity.IR.Should().BeGreaterOrEqualTo(0);
            entity.IR.Should().Be(entity.Profitability() * irPercent);
        }
        [Theory]
        [MemberData(nameof(DataProfitabilityZero))]
        public void ShouldBeProcessCorrectlyIRWhenProfitabilityZero<T>(T entity) where T : InvestmentBase => entity.IR.Should().Be(0);

        [Theory]
        [MemberData(nameof(PurchaseAndDueDatesTo6Percent))]
        public void ShouldBeLoss6PercentFromTotalValue<T>(T entity) where T : InvestmentBase
        {
            entity.RescueValue.Should().BeLessThan(entity.TotalValue);
            entity.RescueValue.Should().BeLessOrEqualTo(entity.TotalValue * RescueTax.THREE_MONTHS);
        }

        [Theory]
        [MemberData(nameof(BaseDataTest.PurchaseAndDueDatesTo15Percent))]
        public void ShouldBeLoss15PercentFromTotalValue<T>(T entity) where T : InvestmentBase
        {
            entity.RescueValue.Should().BeLessThan(entity.TotalValue);
            entity.RescueValue.Should().BeLessOrEqualTo(entity.TotalValue * RescueTax.HALF);
        }

        [Theory]
        [MemberData(nameof(PurchaseAndDueDatesTo30Percent))]
        public void ShouldBeLoss30PercentFromTotalValue<T>(T entity) where T : InvestmentBase
        {
            entity.RescueValue.Should().BeLessThan(entity.TotalValue);
            entity.RescueValue.Should().BeLessOrEqualTo(entity.TotalValue * RescueTax.OTHER);
        }
    }
}
