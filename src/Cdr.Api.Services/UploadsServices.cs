using Cdr.Api.Services.CsvMappings;
using Cdr.Api.Services.Models;
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

        /// <inheritdoc cref="IUploadsServices.UploadCallRecords(IFormFile)" />
        public async Task<CsvParsingResults<CallRecord>> UploadCallRecords(IFormFile file)
        {
            var records = _csvServices.Read<CallRecord, CallRecordMapping>(file.OpenReadStream());
            var dedupredRecords = _csvServices.DeduplicateCallReferences(records);
            await _callRecordRepository.SaveCallRecords(dedupredRecords.ValidRecords);
            return dedupredRecords;
        }



        /// <inheritdoc cref="IUploadsServices.Flush"/>
        public async Task Flush()
        {
            await _callRecordRepository.Flush();
        }
    }
}
