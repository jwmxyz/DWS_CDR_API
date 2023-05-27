namespace Cdr.SharedLibrary
{
    public class StatisticsDTO
    {
        public decimal TotalCostPence { get; set; }
        public decimal AverageCostPence { get; set; }
        public uint AverageDurationSeconds { get; set; }
        public CallRecordDTO LongestCall { get; set; }
        public CallRecordDTO ShortestCall { get; set; }

    }
}