using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOLIST.Models;

namespace TODOLIST.ViewModels
{
    public class TodoViewModel
    {
        public IEnumerable<TodoItem> Items { get; set; }
    }
}
