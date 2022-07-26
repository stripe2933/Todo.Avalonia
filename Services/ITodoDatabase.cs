using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services;

public interface ITodoDatabase : IDisposable
{
    public IEnumerable<TodoItem> GetItems(bool containFinished = true);
    public Task<long> AddItemAsync(TodoItem model);
    public Task DeleteItemAsync(long id);
    public Task UpdateItemAsync<T>(long id, Expression<Func<TodoItem, T>> property, T value);
}