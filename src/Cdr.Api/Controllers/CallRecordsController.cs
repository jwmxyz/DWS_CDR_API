using Cdr.Api.Services;
using Cdr.SharedLibrary;
using Microsoft.AspNetCore.Mvc;

namespace Cdr.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CallRecordsController : SharedController<CallRecordsController>
    {
        private readonly ICallRecordServices _callRecordServices;
        public CallRecordsController(ICallRecordServices callRecordServices)
        {
            _callRecordServices = callRecordServices;
        }

        [HttpGet, Route("reference/{callreference}")]
        public async Task<IActionResult> GetCall(string callreference)
        {
            return Ok(await _callRecordServices.GetCallRecordByReference(callreference));
        }

        [HttpGet, Route("number/{number}")]
        public async Task<IActionResult> GetCall(long number)
        {
            return Ok(await _callRecordServices.GetCallRecordsForNumber(number));
        }

        [HttpGet, Route("Duration/Min")]
        public async Task<IActionResult> MinDuration()
        {
            return Ok(await _callRecordServices.GetCallDurationAsc());
        }

        [HttpGet, Route("Duration/Max")]
        public async Task<IActionResult> MaxDuration()
        {
            return Ok(await _callRecordServices.GetCallDurationDesc());
        }

        [HttpGet, Route("Duration/Min/count/{count}")]
        public async Task<IActionResult> MinDuration(int count)
        {
            return Ok(await _callRecordServices.GetCallDurationAsc(count));
        }

        [HttpGet, Route("Duration/Max/count/{count}")]
        public async Task<IActionResult> MaxDuration(int count)
        {
            return Ok(await _callRecordServices.GetCallDurationDesc(count));
        }

        [HttpGet, Route("Cost/Min")]
        public async Task<IActionResult> MinCost()
        {
            return Ok(await _callRecordServices.GetCallCostAsc());
        }

        [HttpGet, Route("Cost/Max")]
        public async Task<IActionResult> MaxCost()
        {
            return Ok(await _callRecordServices.GetCallCostDesc());
        }

        [HttpGet, Route("Cost/Min/count/{count}")]
        public async Task<IActionResult> MinCost(int count)
        {
            return Ok(await _callRecordServices.GetCallCostAsc(count));
        }

        [HttpGet, Route("Cost/Max/count/{count}")]
        public async Task<IActionResult> MaxCost(int count)
        {
            return Ok(await _callRecordServices.GetCallCostDesc(count));
        }


        [HttpGet, Route("statistics")]
        public async Task<IActionResult> Statistics()
        {
            return Ok(await _callRecordServices.GetStatistics());
        }

        [HttpPost, Route("between-dates")]
        public async Task<IActionResult> DateFinder(BetweenDatesDTO dates)
        {
            return Ok(await _callRecordServices.CallsBetweenDates(dates.DateFrom, dates.DateTo));
        }
    }
}
