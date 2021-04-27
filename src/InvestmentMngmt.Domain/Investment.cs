using InvestmentMngmt.Domain.External;
using System;
using System.Text.Json.Serialization;

namespace InvestmentMngmt.Domain
{
    public class Investment
    {        
        [JsonPropertyName("nome")]
        public string Name { get; init; }

        [JsonPropertyName("valorInvestido")]
        public decimal InvestedAmount { get; init; }

        [JsonPropertyName("valorTotal")]
        public decimal TotalValue { get; init; }

        [JsonPropertyName("vencimento")]
        public DateTime DueDate { get; init; }

        [JsonPropertyName("ir")]
        public decimal IR { get; init; }

        [JsonPropertyName("valorResgate")]
        public decimal RescueValue { get; init; }
        
        public static implicit operator Investment(InvestmentBase investment)
        {
            return new Investment
            {
                Name = investment.Name,
                InvestedAmount = investment.InvestedAmount,
                TotalValue = investment.TotalValue,
                DueDate = investment.DueDate,
                IR = investment.IR,
                RescueValue = investment.RescueValue
            };
        }
    }
}
