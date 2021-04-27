using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InvestmentMngmt.Domain.External.Dtos
{
    public readonly struct FixedIncomeDto
    {
        [JsonPropertyName("lcis")]
        public IEnumerable<FixedIncome> FixedIncomes { get; init; }
    }
}
