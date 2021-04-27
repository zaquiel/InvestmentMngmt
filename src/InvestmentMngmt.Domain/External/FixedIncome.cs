using System;
using System.Text.Json.Serialization;

namespace InvestmentMngmt.Domain.External
{
    public class FixedIncome: InvestmentBase
    {
        [JsonPropertyName("capitalInvestido")]
        public override decimal InvestedAmount { get; init; }

        [JsonPropertyName("capitalAtual")]
        public override decimal TotalValue { get; init; }

        [JsonPropertyName("vencimento")]
        public override DateTime DueDate { get; init; }

        [JsonPropertyName("dataOperacao")]
        public override DateTime PurchaseDate { get; init; }
        

        public override decimal IR => Profitability() >= 0 ? Profitability() * IRPercents.LCIS : 0;
    }
}
