using Crd.DataAccess.Migrations;
using Crd.DataAccess.Migrations.DbModels;

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
            _dbContext.CallRecords.AddRange(callRecords);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}