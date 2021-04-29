using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InvestmentMngmt.Domain.External.Dtos
{
    public readonly struct FundDto
    {
        [JsonPropertyName("fundos")]
        public IEnumerable<Fund> Funds { get; init; }
    }
}
