namespace ToDo777.DB
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
        public bool IsCompleted { get; set; }
    }
}
