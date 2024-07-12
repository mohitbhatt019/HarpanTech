namespace DataContracts.Domain.Entities
{
    public class ReportsWorker
    {
        public int Id { get; set; }
        public string ReportName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}
