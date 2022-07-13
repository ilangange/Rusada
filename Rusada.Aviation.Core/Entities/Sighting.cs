namespace Rusada.Aviation.Core.Entities
{
    public class Sighting : AuditableEntity
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Registration { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string FileData { get; set; }
    }
}
