namespace DataContracts.Domain.Entities
{
    public class Tracking
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Item { get; set; }
        public DateTime DateTime { get; set; }
    }
}
