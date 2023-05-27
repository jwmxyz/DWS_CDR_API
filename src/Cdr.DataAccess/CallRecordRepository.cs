using Crd.DataAccess.Migrations;
using Crd.DataAccess.Migrations.DbModels;
using EFCore.BulkExtensions;

namespace Cdr.DataAccess
{
    public class CallRecordRepository : ICallRecordRepository
    {
        private readonly CdrDbContext _dbContext;

        public CallRecordRepository(CdrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc cref="ICallRecordRepository.SaveCallRecords(IEnumerable{CallRecord})" />
        public async Task<bool> SaveCallRecords(IEnumerable<CallRecord> callRecords)
        {
            _dbContext.BulkInsert(callRecords);
            await _dbContext.BulkSaveChangesAsync();
            return true;
        }

        /// <inheritdoc cref="ICallRecordRepository.Flush" />
        public async Task Flush()
        {
            _dbContext.BulkDelete(_dbContext.CallRecords);
            await _dbContext.BulkSaveChangesAsync();
        }
    }
}