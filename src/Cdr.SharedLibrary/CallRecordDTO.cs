using Crd.DataAccess.Migrations.DbModels;

namespace Cdr.SharedLibrary
{
    public class CallRecordDTO
    {
        public long CallerId { get; set; }
        public long RecipientId { get; set; }
        public DateOnly CallDate { get; set; }
        public TimeOnly EndTime { get; set; }
        public uint DurationSeconds { get; set; }
        public decimal CostPence { get; set; }
        public string Reference { get; set; }
        public string CurrencyIsoCode { get; set; }

        public CallRecordDTO(CallRecord callRecord)
        {
            CallerId = callRecord.CallerId;
            RecipientId = callRecord.RecipientId;
            CallDate = callRecord.CallDate;
            EndTime = callRecord.EndTime;
            DurationSeconds = callRecord.DurationSeconds;
            CostPence = callRecord.CostPence;
            Reference = callRecord.Reference;
            CurrencyIsoCode = callRecord.CurrencyIsoCode;
        }
    }
}
