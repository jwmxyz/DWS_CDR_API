using Cdr.SharedLibrary;
using Crd.DataAccess.Migrations.DbModels;

namespace Cdr.DataAccess
{
    public interface ICallRecordRepository
    {
        /// <summary>
        /// Method used to get a specific call from the reference
        /// </summary>
        /// <param name="reference">the reference we want to find</param>
        /// <returns>The callrecord if found false otherwise.</returns>
        Task<CallRecord> GetCallByReferenceId(string reference);

        /// <summary>
        /// Method used to add a list of call records to the database
        /// </summary>
        /// <param name="callRecords">The records we want to add to the database</param>
        /// <returns>true if the save was successfull false otherwise.</returns>
        Task<bool> SaveCallRecords(IEnumerable<CallRecord> callRecords);

        /// <summary>
        /// Method used to get some general statistics of the dataset
        /// </summary>
        /// <returns>A statistics object</returns>
        Task<StatisticsDTO> CallRecordStatistics();

        /// <summary>
        /// Method used to get al lthe call records for a specific number
        /// </summary>
        /// <param name="number"></param>
        /// <returns>List of calls made by the number</returns>
        Task<List<CallRecord>> GetCallRecordsByNumber(long number);

        /// <summary>
        /// Method used to get calls based on cost
        /// </summary>
        /// <param name="sortDescending">whether descending or not</param>
        /// <param name="count">The number ofd records to return</param>
        /// <returns>List of call records</returns>
        Task<List<CallRecord>> GetCallsByCost(bool sortDescending, int count);

        /// <summary>
        /// Method used to get calls based on duration
        /// </summary>
        /// <param name="sortDescending">whether descending or not</param>
        /// <param name="count">The number ofd records to return</param>
        /// <returns>List of call records</returns>
        Task<List<CallRecord>> GetCallsByDuration(bool sortDescending, int count);

        /// <summary>
        /// Method used to get call records between 2 dates
        /// </summary>
        /// <param name="from">The calls from date</param>
        /// <param name="to">The calls to date</param>
        /// <returns>A list of calls</returns>
        Task<List<CallRecord>> CallsBetweenDates(DateTime from, DateTime to);

        /// <summary>
        /// Method used to flush all data for testing
        /// N.B This would not exist in a production sytem
        /// </summary>
        Task Flush();
    }
}