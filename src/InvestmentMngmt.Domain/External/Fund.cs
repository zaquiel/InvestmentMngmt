using System;
using System.Text.Json.Serialization;

namespace InvestmentMngmt.Domain.External
{
    public class Fund: InvestmentBase
    {
        [JsonPropertyName("capitalInvestido")]
        public override decimal InvestedAmount { get; init; }

        [JsonPropertyName("ValorAtual")]
        public override decimal TotalValue { get; init; }

        [JsonPropertyName("dataResgate")]
        public override DateTime DueDate { get; init; }

        [JsonPropertyName("dataCompra")]
        public override DateTime PurchaseDate { get; init; }
        

        public override decimal IR => Profitability() >= 0 ? Profitability() * IRPercents.FUNDS : 0;
    }
}
