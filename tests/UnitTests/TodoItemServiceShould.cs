using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOLIST.Data;
using TODOLIST.Models;
using TODOLIST.Services;
using Xunit;

namespace UnitTests
{
    public class TodoItemServiceShould
    {
        [Fact]
        public async Task AddNewItemAsIncompleteWithDueDate()
        {
            //To Configure in memory DB
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_AddNewItem").Options;
            // Set up a context (connection to the "DB") for writing
            using (var context = new ApplicationDbContext(options))
            {
                var service = new TodoItemService(context);
                var fakeUser = CreateFakeUser();
                await service.AddItemAsync(new TodoItem
                {
                    Title = "Testing?"
                }, fakeUser);
            }
            // Use a separate context to read data back from the "DB"
            using (var context = new ApplicationDbContext(options))
            {
                var itemsInDatabase = await context
                .Items.CountAsync();
                Assert.Equal(1, itemsInDatabase);
                var item = await context.Items.FirstAsync();
                Assert.Equal("Testing?", item.Title);
                Assert.False(item.IsDone);
                // Item should be due 3 days from now (give or take a second)
                var difference = DateTimeOffset.Now.AddDays(3) - item.DueAt;
                Assert.True(difference < TimeSpan.FromSeconds(1));

            }
     
        }
        /// <summary>
        /// The MarkDoneAsync() method returns true when it makes a valid item as complete
        /// </summary>
        [Fact]
        public async Task MarkAsCompleteWithDueDate()
        {
            //To Configure in memory DB
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_AddNewItem").Options;

            using (var context = new ApplicationDbContext(options))
            {
                var service = new TodoItemService(context);
                var item = await context.Items.FirstAsync();
                await service.MarkDoneAsync(item.Id, CreateFakeUser());

             
                Assert.True(item.IsDone);
                // Item should be due 3 days from now (give or take a second)
                var difference = DateTimeOffset.Now.AddDays(3) - item.DueAt;
                Assert.True(difference < TimeSpan.FromSeconds(1));

            }

        }
        /// <summary>
        /// The MarkDoneAsync() method returns false if it's passed an ID that doesn't exist
        /// </summary>
        [Fact]
        public async Task MarkAsCompleteWithWrongID()
        {
            //To Configure in memory DB
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_AddNewItem").Options;

            using (var context = new ApplicationDbContext(options))
            {
                var service = new TodoItemService(context);

               var result= await service.MarkDoneAsync(Guid.NewGuid(), CreateFakeUser());

                Assert.False(result);

            }

        }

        /// <summary>
        /// The GetIncompleteItemsAsync() method returns only the items owned by a particular user
        /// </summary>
        [Fact]
        public async Task GetIncompleteItems()
        {
            //To Configure in memory DB
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_AddNewItem").Options;

            using (var context = new ApplicationDbContext(options))
            {
                var service = new TodoItemService(context);

                IEnumerable<TodoItem> result = await service.GetIncompleteItemsAsync(CreateFakeUser());
                var item=result.First();
                Assert.Equal(item.UserId,CreateFakeUser().Id);
            }

        }

        private IdentityUser CreateFakeUser()
        {
            return new IdentityUser
            {
                Id = "fake-000",
                UserName = "fake@example.com"
            };
        }

    }
}
