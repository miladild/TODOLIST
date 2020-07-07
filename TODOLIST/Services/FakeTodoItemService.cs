using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOLIST.Models;

namespace TODOLIST.Services
{
    public class FakeTodoItemService:ITodoItemService
    {
        public Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync()
        {
            IEnumerable<TodoItem> items = new List<TodoItem>()
            {
                new TodoItem {Title="Learn ASP.NET Core",DueAt= DateTimeOffset.Now.AddDays(1) },
                 new TodoItem {Title="Learn ASP.NET Core",DueAt= DateTimeOffset.Now.AddDays(1) }
            };


            return Task.FromResult(items);
        }


    }
}
