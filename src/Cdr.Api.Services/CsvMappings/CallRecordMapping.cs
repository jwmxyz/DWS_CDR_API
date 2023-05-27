using Crd.DataAccess.Migrations.DbModels;
using CsvHelper.Configuration;

namespace Cdr.Api.Services.CsvMappings
{
    public sealed class CallRecordMapping : ClassMap<CallRecord>
    {
        public CallRecordMapping()
        {
            Map(x => x.CallerId).Name("caller_id");
            Map(x => x.RecipientId).Name("recipient");
            Map(x => x.CallDate).Name("call_date").TypeConverter<CsvHelper.TypeConversion.DateOnlyConverter>().TypeConverterOption.Format("dd/MM/yyyy");
            Map(x => x.EndTime).Name("end_time");
            Map(x => x.DurationSeconds).Name("duration");
            Map(x => x.CostPence).Name("cost");
            Map(x => x.Reference).Name("reference");
            Map(x => x.CurrencyIsoCode).Name("currency");
        }
    }
}
