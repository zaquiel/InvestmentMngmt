using InvestmentMngmt.Domain.External;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvestmentMngmt.Domain.External.Dtos
{
    public readonly struct TreasuryDirectDto
    {
        [JsonPropertyName("tds")]
        public IEnumerable<TreasuryDirect> TreasuryDirects { get; init; }
    }
}