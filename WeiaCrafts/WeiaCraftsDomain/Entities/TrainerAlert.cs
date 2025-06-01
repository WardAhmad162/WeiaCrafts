namespace WeiaCraftsDomain.Entities;

public class TrainerAlert
{
    
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public required string TrainerUserName { get; set; }
        public Trainer Trainer { get; set; } = null!;

}