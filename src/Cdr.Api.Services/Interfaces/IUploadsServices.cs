using Microsoft.AspNetCore.Http;

namespace Cdr.Api.Services
{
    public interface IUploadsServices
    {
        /// <summary>
        /// Method used to upload a file to the database
        /// </summary>
        /// <param name="file">The csv file we wish to upload to the datbase</param>
        /// <returns>true if success, false otherwise</returns>
        Task<bool> Upload(IFormFile file);
    }
}