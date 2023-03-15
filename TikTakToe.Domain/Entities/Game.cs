namespace TikTakToe.Domain.Entities
{
    public class Game: BaseEntity
    {
        public int? WinnerId { get; set; }
        public User? Winner { get; set; }
    }
}
