using System.Collections.Generic;

namespace ReazorTemplate.Models
{
    public class TodoListViewModel
    {
        public List<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}
