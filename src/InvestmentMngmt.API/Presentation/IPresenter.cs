using InvestmentMngmt.Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestmentMngmt.API.Presentation
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPresenter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        IActionResult GetActionResult<T>(Response<T> response);
    }
}
