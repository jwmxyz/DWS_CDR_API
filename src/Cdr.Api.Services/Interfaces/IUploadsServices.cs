using Cdr.Api.Services.Models;
using Crd.DataAccess.Migrations.DbModels;
using Microsoft.AspNetCore.Http;

namespace Cdr.Api.Services
{
    public interface IUploadsServices
    {
        /// <summary>
        /// Method used to upload a file to the database
        /// </summary>
        /// <param name="file">The csv file we wish to upload to the datbase</param>
        /// <returns>object containing the csvParsing results</returns>
        Task<CsvParsingResults<CallRecord>> UploadCallRecords(IFormFile file);

        /// <summary>
        /// Method used to flush all data for testing
        /// N.B This would not exist in the 
        /// </summary>
        Task Flush();
    }
}