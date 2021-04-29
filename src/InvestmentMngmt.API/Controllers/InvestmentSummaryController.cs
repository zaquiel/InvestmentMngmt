using InvestmentMngmt.API.Presentation;
using InvestmentMngmt.Application.Investment.Commands.Get;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestmentMngmt.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]    
    public class InvestmentSummaryController: Controller
    {
        private readonly ISender _mediator;
        private readonly IPresenter _presenter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="presenter"></param>
        public InvestmentSummaryController(ISender mediator, IPresenter presenter)
        {
            _mediator = mediator;
            _presenter = presenter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Application.Investment.Commands.Get.Result))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpResponseError))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(HttpResponseError))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(HttpResponseError))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpResponseError))]
        public async Task<IActionResult> Get()
        {
            return _presenter.GetActionResult(await _mediator.Send(new Request()));            
        }      

        //[HttpGet("v1/portfolio")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InvestmentsResponse))]
        //public async Task<IActionResult> Get()
        //    => _presenter.GetActionResult(await _mediator.Send(new InvestmentsRequest()));
    }
}
