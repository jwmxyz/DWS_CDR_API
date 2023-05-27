using Cdr.Api.Services.Models;
using Crd.DataAccess.Migrations.DbModels;
using CsvHelper.Configuration;

namespace Cdr.Api.Services
{
    public interface ICsvServices
    {
        /// <summary>
        /// Method used to read in a stream from a csv and convert into a list of objects
        /// </summary>
        /// <typeparam name="T">The object type each entry should be parsed to</typeparam>
        /// <typeparam name="K">The object representing the mappings of incoming CSVs to an object</typeparam>
        /// <param name="file">The stream of the input file</param>
        /// <returns>A list of parsed objects from the csv</returns>
        CsvParsingResults<T> Read<T, K>(Stream file, bool firstRowIsHeading = true) where K : ClassMap<T>;

        /// <summary>
        /// A simple method to remove duplicate data from the call records and update the errors array
        /// </summary>
        /// <param name="results">the results we want to check</param>
        /// <returns>the update reuslts object</returns>
        CsvParsingResults<CallRecord> DeduplicateCallReferences(CsvParsingResults<CallRecord> results);
    }
}