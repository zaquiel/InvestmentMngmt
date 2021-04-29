using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace InvestmentMngmt.Application.Investment.Commands.Get
{
    public class Result
    {
        [JsonPropertyName("valorTotal")]
        public decimal TotalValue => Investments.Sum(i => i.TotalValue);

        [JsonPropertyName("investimentos")]
        public IEnumerable<Domain.Investment> Investments { get; init; }
        
        public Result(IEnumerable<Domain.Investment> investments) => Investments = investments;
    }
}
