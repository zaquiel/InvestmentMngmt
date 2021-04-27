using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentMngmt.Domain.Settings
{
    public record ExternalServicesSettings
    {
        public string BaseUri { get; set; }
        public ExternalServicesSettingsUris Uris { get; set; }
    }

    public record ExternalServicesSettingsUris
    { 
        public string TreasuryDirect { get; set; }
        public string FixedIncome { get; set; }
        public string Funds { get; set; }
    }
}
