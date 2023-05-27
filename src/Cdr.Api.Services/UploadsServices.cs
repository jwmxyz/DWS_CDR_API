using Cdr.Api.Services.CsvMappings;
using Cdr.DataAccess;
using Crd.DataAccess.Migrations.DbModels;
using Microsoft.AspNetCore.Http;

namespace Cdr.Api.Services
{
    public class UploadsServices : IUploadsServices
    {
        private readonly ICsvServices _csvServices;
        private readonly ICallRecordRepository _callRecordRepository;

        public UploadsServices(ICsvServices csvServices, ICallRecordRepository callRecordRepository)
        {
            _csvServices = csvServices;
            _callRecordRepository = callRecordRepository;
        }

        /// <inheritdoc cref="IUploadsServices.Upload(IFormFile)" />
        public async Task<bool> Upload(IFormFile file)
        {
            var records = _csvServices.Read<CallRecord, CallRecordMapping>(file.OpenReadStream());
            var result = await _callRecordRepository.SaveCallRecords(records);
            return result;
        }
    }
}
