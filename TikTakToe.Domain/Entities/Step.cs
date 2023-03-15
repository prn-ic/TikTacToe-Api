namespace TikTakToe.Domain.Entities
{
    public class Step: BaseEntity
    {
        public int StepIndex { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public User? User { get; set; }
    }
}
