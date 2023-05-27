namespace Cdr.Api.Services.Models
{
    public class CsvParsingResults<T>
    {
        public List<string> ParsingErrors { get; set; }
        public List<T> ValidRecords { get; set; }
        public int TotalRecords => ValidRecords.Count + ParsingErrors.Count;
        public int SavedRecords => ValidRecords.Count;
    }
}
