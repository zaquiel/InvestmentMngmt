using InvestmentMngmt.Application.Investment.Commands.Get;
using InvestmentMngmt.Domain;
using InvestmentMngmt.Domain.Constants;
using InvestmentMngmt.Domain.Settings;
using System;
using System.Collections.Generic;

namespace InvestmentMngmt.Tests
{
    public class BaseTest
    {
        protected string DistributedCacheKey => $"{DistributedCacheKeys.InvestmentPortfolio}{DateTime.Now.ToShortDateString()}";

        private readonly static List<Investment> _investments = new()
        {
            new Investment
            {
                Name = "Test Investment",
                InvestedAmount = 12.57M,
                TotalValue = 16.85M,
                DueDate = DateTime.Now.AddMonths(12),
                IR = 0.65M,
                RescueValue = 14.35M
            }
        };

        protected static ExternalServicesSettings ExternalServicesSettings =>
            new()
            {
                BaseUri = "http://www.mocky.io/v2",
                Uris = new ExternalServicesSettingsUris
                { 
                    TreasuryDirect = "5e3428203000006b00d9632a",
                    FixedIncome = "5e3428203000006b00d9632a",
                    Funds = "5e342ab33000008c00d96342"
                }
            };
        protected static Result InvestmentsResult => new(_investments);
    }
}
