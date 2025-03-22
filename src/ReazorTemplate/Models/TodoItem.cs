using System;

namespace ReazorTemplate.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsComplete { get; set; }
        public DateTime? CompleteTime { get; set; }
    }
}
