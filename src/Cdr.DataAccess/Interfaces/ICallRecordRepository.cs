using Crd.DataAccess.Migrations.DbModels;

namespace Cdr.DataAccess
{
    public interface ICallRecordRepository
    {
        /// <summary>
        /// Method used to add a list of call records to the database
        /// </summary>
        /// <param name="callRecords">The records we want to add to the database</param>
        /// <returns>true if the save was successfull false otherwise.</returns>
        Task<bool> SaveCallRecords(IEnumerable<CallRecord> callRecords);

        /// <summary>
        /// Method used to flush all data for testing
        /// N.B This would not exist in a production sytem
        /// </summary>
        Task Flush();
    }
}