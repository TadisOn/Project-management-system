namespace PMS.Data.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required DateTime CreationDate { get; set; }

        public required Project Project { get; set; }
    }
}
