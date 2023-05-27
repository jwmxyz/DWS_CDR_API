using Cdr.SharedLibrary;
using Crd.DataAccess.Migrations.DbModels;

namespace Cdr.Api.Services
{
    public interface ICallRecordServices
    {
        /// <summary>
        /// Method used to call the database and return the call record by id
        /// </summary>
        /// <param name="reference">the callrecord we want to find</param>
        /// <returns>the call record if found 404 otherwise</returns>
        Task<CallRecord> GetCallRecordByReference(string reference);

        /// <summary>
        /// Method used to get some general statistics of the dataset
        /// </summary>
        /// <returns></returns>
        Task<StatisticsDTO> GetStatistics();
        
        /// <summary>
        /// Method used to get al lthe call records for a specific number
        /// </summary>
        /// <param name="number"></param>
        /// <returns>List of calls made by the number</returns>
        Task<List<CallRecord>> GetCallRecordsForNumber(long number);

        /// <summary>
        /// Method used to get a list of calls descending based on duration
        /// </summary>
        /// <param name="count">The number of records to return</param>
        /// <returns>List of call records</returns>
        Task<List<CallRecord>> GetCallDurationDesc(int count = 1);

        /// <summary>
        /// Method used to get a list of calls ascending based on duration
        /// </summary>
        /// <param name="count">The number of records to return</param>
        /// <returns>List of call records</returns>
        Task<List<CallRecord>> GetCallDurationAsc(int count = 1);

        /// <summary>
        /// Method used to get a list of calls descending based on cost
        /// </summary>
        /// <param name="count">The number of records to return</param>
        /// <returns>List of call records</returns>
        Task<List<CallRecord>> GetCallCostDesc(int count = 1);

        /// <summary>
        /// Method used to get a list of calls ascending based on cost
        /// </summary>
        /// <param name="count">The number of records to return</param>
        /// <returns>List of call records</returns>
        Task<List<CallRecord>> GetCallCostAsc(int count = 1);

        /// <summary>
        /// Method used to get call records between 2 dates
        /// </summary>
        /// <param name="from">The calls from date</param>
        /// <param name="to">The calls to date</param>
        /// <returns>A list of calls</returns>
        Task<List<CallRecord>> CallsBetweenDates(DateTime from, DateTime to);

    }
}