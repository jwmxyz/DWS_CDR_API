using Cdr.DataAccess;
using Cdr.ErrorManagement;
using Cdr.ErrorManagement.Exceptions;
using Cdr.SharedLibrary;
using Crd.DataAccess.Migrations.DbModels;

namespace Cdr.Api.Services
{
    public class CallRecordServices : ICallRecordServices
    {
        private readonly ICallRecordRepository _callRecordRepository;
        private readonly ICdrErrorManager _cdrErrorManager;

        public CallRecordServices(ICallRecordRepository callRecordRepository, ICdrErrorManager cdrErrorManager)
        {
            _callRecordRepository = callRecordRepository;
            _cdrErrorManager = cdrErrorManager;
        }

        /// <inheritdoc cref="ICallRecordServices.GetCallRecordByReference(string)"/>
        public async Task<CallRecord> GetCallRecordByReference(string reference)
        {
            var result = await _callRecordRepository.GetCallByReferenceId(reference);
            if (result == null)
            {
                throw _cdrErrorManager.LogWarningAndReturnException<NotFoundException>($"Call Record with Reference {reference} was not found");
            }
            return result;
        }

        /// <inheritdoc cref="ICallRecordServices.GetCallRecordsForNumber(long)"/>
        public async Task<List<CallRecord>> GetCallRecordsForNumber(long number)
        {
            var result = await _callRecordRepository.GetCallRecordsByNumber(number);
            if (result == null || !result.Any())
            {
                throw _cdrErrorManager.LogWarningAndReturnException<NotFoundException>($"Call Records for number {number} was not found");
            }
            return result;
        }

        /// <inheritdoc cref="ICallRecordServices.GetCallDurationDesc(int)"/>
        public async Task<List<CallRecord>> GetCallDurationDesc(int count = 1)
        {
            return await GetCallDuration(true, count);
        }

        /// <inheritdoc cref="ICallRecordServices.GetCallDurationAsc(int)"/>
        public async Task<List<CallRecord>> GetCallDurationAsc(int count = 1)
        {
            return await GetCallDuration(false, count);
        }

        /// <inheritdoc cref="ICallRecordServices.GetCallCostDesc(int)"/>
        public async Task<List<CallRecord>> GetCallCostDesc(int count = 1)
        {
            return await GetCallCost(true, count);
        }

        /// <inheritdoc cref="ICallRecordServices.GetCallCostAsc(int)"/>
        public async Task<List<CallRecord>> GetCallCostAsc(int count = 1)
        {
            return await GetCallCost(false, count);
        }

        /// <inheritdoc cref="ICallRecordServices.GetStatistics"/>
        public async Task<StatisticsDTO> GetStatistics()
        {
            return await _callRecordRepository.CallRecordStatistics();
        }

        /// <inheritdoc cref="ICallRecordServices.CallsBetweenDates"/>
        public async Task<List<CallRecord>> CallsBetweenDates(DateTime from, DateTime to)
        {
            return await _callRecordRepository.CallsBetweenDates(from, to);
        }

        /// <summary>
        /// Method used to get the call records using the cost
        /// </summary>
        /// <param name="isDesc">whether to order descending</param>
        /// <param name="count">The number to return</param>
        /// <returns>A list, count length of call records based on cost</returns>
        private async Task<List<CallRecord>> GetCallCost(bool isDesc, int count)
        {
            return await _callRecordRepository.GetCallsByCost(isDesc, count);
        }

        /// <summary>
        /// Method used to get the call records using the duration
        /// </summary>
        /// <param name="isDesc">whether to order descending</param>
        /// <param name="count">The number to return</param>
        /// <returns>A list, count length of call records based on duration</returns>
        private async Task<List<CallRecord>> GetCallDuration(bool isDesc, int count)
        {
            return await _callRecordRepository.GetCallsByDuration(isDesc, count);
        }
    }
}
