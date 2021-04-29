using Hangfire.Dashboard;

namespace InvestmentMngmt.API.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class AllowAllConnectionsFilter: IDashboardAuthorizationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool Authorize(DashboardContext context) => true;
    }
}
