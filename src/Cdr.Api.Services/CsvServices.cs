using Cdr.Api.Services.Models;
using Cdr.ErrorManagement;
using Cdr.ErrorManagement.Exceptions;
using Crd.DataAccess.Migrations.DbModels;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Cdr.Api.Services
{
    public class CsvServices : ICsvServices
    {
        private readonly ICdrErrorManager _cdrErrorManager;

        public CsvServices(ICdrErrorManager cdrErrorManager)
        {
            _cdrErrorManager = cdrErrorManager;
        }

        /// <inheritdoc cref="ICsvServices.Read{T, K}(Stream, bool)" />
        public CsvParsingResults<T> Read<T, K>(Stream file, bool firstRowIsHeading = true) where K : ClassMap<T>
        {
            try
            {
                var reader = new StreamReader(file);
                var errors = new List<string>();
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = firstRowIsHeading,
                    ReadingExceptionOccurred = e =>
                    {
                        errors.Add(e.Exception.Message);
                        _cdrErrorManager.LogErrorAndReturnException<Exception>(e.Exception.Message, e.Exception);
                        return false;
                    }
                };
                var csv = new CsvReader(reader, configuration);
                csv.Context.RegisterClassMap<K>();
                var records = csv.GetRecords<T>().ToList();
                return new CsvParsingResults<T>
                {
                    ParsingErrors = errors,
                    ValidRecords  = records
                };
            }
            catch (Exception ex)
            {
                throw _cdrErrorManager.LogErrorAndReturnException<InvalidCsvException>("Invalid CSV - Error When parsing", ex.InnerException);
            }
        }

        /// <inheritdoc cref="ICsvServices.DeduplicateCallReferences(CsvParsingResults{CallRecord})" />
        public CsvParsingResults<CallRecord> DeduplicateCallReferences(CsvParsingResults<CallRecord> results)
        {
            var duplicateItems = results.ValidRecords.GroupBy(x => x.Reference).Where(x => x.Count() > 1);
            foreach (var item in duplicateItems)
            {
                results.ParsingErrors.Add($"Removed calls with reference {item.Key} as duplicates exist");
            }
            results.ValidRecords = results.ValidRecords.GroupBy(x => x.Reference).Where(x => x.Count() == 1).SelectMany(x => x.ToList()).ToList();
            return results;
        }
    }
}