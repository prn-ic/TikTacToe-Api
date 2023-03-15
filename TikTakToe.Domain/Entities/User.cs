namespace TikTakToe.Domain.Entities
{
    public class User: BaseEntity
    {
        public string? Name { get; set; }
        public int WinningCount { get; set; }
    }
}
