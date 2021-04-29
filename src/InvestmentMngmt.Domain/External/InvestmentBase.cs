using System;
using System.Text.Json.Serialization;

namespace InvestmentMngmt.Domain.External
{
    public abstract class InvestmentBase
    {
        [JsonPropertyName("nome")]
        public string Name { get; init; }
        
        public abstract decimal InvestedAmount { get; init; }

        public abstract decimal TotalValue { get; init; }
       
        public abstract DateTime DueDate { get; init; }

        public abstract DateTime PurchaseDate { get; init; }

        public abstract decimal IR { get; }

        public decimal RescueValue
        {
            get
            {
                if (DueDate.Date <= DateTime.Now)
                    return TotalValue;

                var totalSeconds = (DateTime.Now - PurchaseDate).TotalSeconds;
                var percent = totalSeconds / (DueDate - PurchaseDate).TotalSeconds;

                if ((DueDate - DateTime.Now).Days <= 90)
                    return TotalValue * RescueTax.THREE_MONTHS;

                if (percent > 0.5)
                    return TotalValue * RescueTax.HALF;

                return TotalValue * RescueTax.OTHER;
            }
        }
        
        public decimal Profitability() => TotalValue - InvestedAmount;
    }
}
