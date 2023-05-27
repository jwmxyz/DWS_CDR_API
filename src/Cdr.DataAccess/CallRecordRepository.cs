using Cdr.SharedLibrary;
using Crd.DataAccess.Migrations;
using Crd.DataAccess.Migrations.DbModels;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace Cdr.DataAccess
{
    public class CallRecordRepository : ICallRecordRepository
    {
        private readonly CdrDbContext _dbContext;

        public CallRecordRepository(CdrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc cref="ICallRecordRepository.CallRecordStatistics"/>
        public async Task<StatisticsDTO> CallRecordStatistics()
        {
            if (!_dbContext.CallRecords.Any())
            {
                return new StatisticsDTO();
            }
            var longestCall = (await GetCallsByDuration(true, 1)).FirstOrDefault();
            var shortestCall = (await GetCallsByDuration(false, 1)).FirstOrDefault();
            var averageDuration = await _dbContext.CallRecords.AverageAsync(x => x.DurationSeconds);
            // SQLite cannot apply aggregate operator 'Average' on expressions of type 'decimal'
            var averageCost = await _dbContext.CallRecords.AverageAsync(x => (double) x.CostPence);
            var totalCost = await _dbContext.CallRecords.SumAsync(x => (double) x.CostPence);
            return new StatisticsDTO()
            {
                LongestCall = new CallRecordDTO(longestCall),
                ShortestCall = new CallRecordDTO(shortestCall),
                TotalCostPence = (decimal) totalCost,
                AverageDurationSeconds = (uint)averageDuration,
                AverageCostPence = (decimal) averageCost
            };
        }

        /// <inheritdoc cref="ICallRecordRepository.GetCallsByDuration(bool, count)"/>
        public async Task<List<CallRecord>> GetCallsByCost(bool sortDescending, int count)
        {
            // SQLite cannot apply aggregate operator 'OrderBy' on expressions of type 'decimal'
            if (sortDescending)
            {
                return await _dbContext.CallRecords.OrderByDescending(x => (double)x.CostPence).Take(count).ToListAsync();
            }
            return await _dbContext.CallRecords.OrderBy(x => (double)x.CostPence).Take(count).ToListAsync();
        }

        /// <inheritdoc cref="ICallRecordRepository.GetCallsByDuration(bool, count)"/>
        public async Task<List<CallRecord>> GetCallsByDuration(bool sortDescending, int count)
        {
            if (sortDescending)
            {
                return await _dbContext.CallRecords.OrderByDescending(x => x.DurationSeconds).Take(count).ToListAsync();
            }
            return await _dbContext.CallRecords.OrderBy(x => x.DurationSeconds).Take(count).ToListAsync();
        }

        /// <inheritdoc cref="ICallRecordRepository.GetCallRecordsByNumber(long)" />
        public async Task<List<CallRecord>> GetCallRecordsByNumber(long number)
        {
            return await _dbContext.CallRecords.Where(x => x.CallerId == number).ToListAsync();
        }

        /// <inheritdoc cref="ICallRecordRepository.GetCallByReferenceId(string)" />
        public async Task<CallRecord> GetCallByReferenceId(string reference)
        {
            return await _dbContext.CallRecords.FirstOrDefaultAsync(x => x.Reference.ToLower() == reference.ToLower());
        }

        /// <inheritdoc cref="ICallRecordRepository.SaveCallRecords(IEnumerable{CallRecord})" />
        public async Task<bool> SaveCallRecords(IEnumerable<CallRecord> callRecords)
        {
            _dbContext.BulkInsert(callRecords);
            await _dbContext.BulkSaveChangesAsync();
            return true;
        }

        /// <inheritdoc cref=ICallRecordRepository.CallsBetweenDates(DateTime, DateTimeKind)"/>
        public async Task<List<CallRecord>> CallsBetweenDates(DateTime from, DateTime to)
        {
            if (!_dbContext.CallRecords.Any())
            {
                return new List<CallRecord>();
            }
            return await _dbContext.CallRecords
                .Where(x => x.CallDate >= DateOnly.FromDateTime(from.Date) && x.CallDate <= DateOnly.FromDateTime(to.Date))
                .ToListAsync();
        }

        /// <inheritdoc cref="ICallRecordRepository.Flush" />
        public async Task Flush()
        {
            _dbContext.BulkDelete(_dbContext.CallRecords);
            await _dbContext.BulkSaveChangesAsync();
        }
    }
}