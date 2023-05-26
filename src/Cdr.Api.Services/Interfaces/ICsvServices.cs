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
        List<T> Read<T, K>(Stream file, bool firstRowIsHeading = true) where K : ClassMap<T>;
    }
}