using System;
using System.Text.Json.Serialization;

namespace InvestmentMngmt.Domain.External
{
    public class TreasuryDirect: InvestmentBase
    {
        [JsonPropertyName("valorInvestido")]
        public override decimal InvestedAmount { get; init; }

        [JsonPropertyName("valorTotal")]
        public override decimal TotalValue { get; init; }

        [JsonPropertyName("vencimento")]
        public override DateTime DueDate { get; init; }

        [JsonPropertyName("dataDeCompra")]
        public override DateTime PurchaseDate { get; init; }        

        public override decimal IR => Profitability() >= 0 ? Profitability() * IRPercents.TDS : 0;
    }
}
