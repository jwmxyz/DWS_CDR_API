using Cdr.DataAccess;
using Crd.DataAccess.Migrations;
using Crd.DataAccess.Migrations.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Cdr.DataAcess.Tests
{
    [TestClass]
    public class CallRecordRepositoryTests
    {
        private readonly DbContextOptions<CdrDbContext> _dbContextOptions;

        public CallRecordRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<CdrDbContext>()
                .UseInMemoryDatabase(databaseName: "cdr")
                .Options;
        }


        [TestMethod]
        public async Task When_AddingTwoValidRecords_SaveIsSuccess()
        {
            //arrange 
            var cdrContext = new CdrDbContext(_dbContextOptions);
            var callRecordRepository = new CallRecordRepository(cdrContext);

            //act
            await callRecordRepository.SaveCallRecords(new List<CallRecord>()
            {
                new CallRecord()
                {
                    DurationSeconds = 1,
                    CallDate = new DateOnly(),
                    CallerId = 1,
                    CostPence = 1,
                    CurrencyIsoCode = "GBP",
                    EndTime = new TimeOnly(),
                    RecipientId = 2,
                    Reference = "SomeuniqueReference"
                },
                new CallRecord()
                {
                    DurationSeconds = 1435,
                    CallDate = new DateOnly(),
                    CallerId = 2,
                    CostPence = 0.23m,
                    CurrencyIsoCode = "GBP",
                    EndTime = new TimeOnly(),
                    RecipientId = 23,
                    Reference = "SomeReference"
                }
            });

            //Assert
            Assert.IsTrue(cdrContext.CallRecords.Count() == 2);
        }
    }
}