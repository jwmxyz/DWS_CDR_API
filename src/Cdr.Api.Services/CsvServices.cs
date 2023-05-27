using Cdr.ErrorManagement;
using Cdr.ErrorManagement.Exceptions;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Cdr.Api.Services
{
    public class CsvServices : ICsvServices
    {
        private readonly ICdrErrorManager _cdrErrorManager;

        public CsvServices(ICdrErrorManager cdrErrorManager) {
            _cdrErrorManager = cdrErrorManager;
        }

        /// <inheritdoc cref="ICsvServices.Read{T, K}(Stream, bool)" />
        public List<T> Read<T, K>(Stream file, bool firstRowIsHeading = true) where K : ClassMap<T>
        {
            try
            {
                var reader = new StreamReader(file);
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = firstRowIsHeading,
                };
                var csv = new CsvReader(reader, configuration);
                csv.Context.RegisterClassMap<K>();
                var records = csv.GetRecords<T>().ToList();
                return records;
            } catch (Exception ex)
            {
                throw _cdrErrorManager.LogErrorAndReturnException<InvalidCsvException>("Invalid CSV - Error When parsing", ex.InnerException);
            }
        }
    }
}