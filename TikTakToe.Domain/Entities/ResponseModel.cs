namespace TikTakToe.Domain.Entities
{
    public class ResponseModel<T>: BaseEntity
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
    }
}
