using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOLIST.Models;

namespace TODOLIST.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync();
    }
}
