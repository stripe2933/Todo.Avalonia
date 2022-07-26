using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services;

public class DesignTimeDatabase : ITodoDatabase
{
    private long _lastInsertRowId;
    
    public IEnumerable<TodoItem> GetItems(bool containFinished = true)
    {
        var items = new[]
        {
            new TodoItem(1, "Walk the dog", DateTimeOffset.Now),
            new TodoItem(2, "Buy the milk", DateTimeOffset.Now.AddDays(1), true),
            new TodoItem(3, "Learn Avalonia"),
            new TodoItem(4, "Tennis class", DateTimeOffset.Now.AddDays(3)),
            new TodoItem(5, "PCR test", DateTimeOffset.Now.AddDays(-5), true),
            new TodoItem(6, "Get COVID-19 certification paper", DateTimeOffset.Now.AddDays(-4))
        };
        if (!containFinished)
        {
            items = items.Where(p => !p.IsFinished).ToArray();
        }
        
        _lastInsertRowId = items.Length;
        return items;
    }

    public async Task<long> AddItemAsync(TodoItem model)
    {
        await Task.Delay(new TimeSpan(20));
        _lastInsertRowId++;
        
        return _lastInsertRowId;
    }

    public async Task DeleteItemAsync(long id)
    {
        await Task.Delay(new TimeSpan(20));
    }

    public async Task UpdateItemAsync<T>(long id, Expression<Func<TodoItem, T>> property, T value)
    {
        await Task.Delay(new TimeSpan(25));
    }

    public void Dispose()
    {
        // Nothing to dispose
    }
}