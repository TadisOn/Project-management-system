namespace PMS.Data.Entities
{
    public class Worker
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set;}
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public DateTime CreationDate { get; set; }

        public required Task Task { get; set; }
    }

    public record WorkerDto(int Id, string FirstName, string LastName,string UserName);
}
