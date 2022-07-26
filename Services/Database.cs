using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Todo.Models;

namespace Todo.Services;

public class Database : ITodoDatabase
{
    private readonly SqliteConnection _connection;

    public Database(string sqliteFilename)
    {
        string connectionString = $"Data Source={sqliteFilename}";
        _connection = new SqliteConnection(connectionString);

        _connection.Open();
        
        CreateTableIfNotExists();
    }

    #region Functions

    /// <summary>
    /// Create the to-do databse if it does not exist for given sqliteFilename.
    /// </summary>
    private void CreateTableIfNotExists()
    {
        const string sql = "CREATE TABLE IF NOT EXISTS todotable(" +
                           "id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                           "description TEXT NOT NULL," +
                           "duedate INTEGER," +
                           "isfinished INTEGER NOT NULL CHECK (isfinished = 0 OR isfinished = 1)" +
                           ");";
        
        using var command = new SqliteCommand(sql, _connection);
        command.ExecuteNonQuery();
    }

    // public async IAsyncEnumerable<TodoItem> GetItemsAsync()
    // {
    //     const string sql = "SELECT id, description, duedate, isfinished FROM todotable";
    //     await using var command = new SqliteCommand(sql, _connection);
    //     await using var reader = await command.ExecuteReaderAsync();
    //     
    //     while (await reader.ReadAsync())
    //     {
    //         long id = reader.GetInt64(0);
    //         string description = reader.GetString(1);
    //         
    //         var hasDueDate = !reader.IsDBNull(2);
    //         DateTimeOffset? dueDate = hasDueDate ? reader.GetDateTimeOffset(2) : null;
    //         
    //         bool finished = reader.GetInt64(3) != 0;
    //         
    //         var result = new TodoItem(id, description, dueDate, finished);
    //         yield return result;
    //     }
    //         
    //     await reader.CloseAsync();
    // }
    
    public IEnumerable<TodoItem> GetItems(bool containFinished = true)
    {
        string sql;
        if (containFinished)
        {
            sql = "SELECT id, description, duedate, isfinished FROM todotable";
        }
        else
        {
            sql = "SELECT id, description, duedate, isfinished FROM todotable WHERE isfinished = 0";
        }
        using var command = new SqliteCommand(sql, _connection);
        using var reader = command.ExecuteReader();
        
        while (reader.Read())
        {
            long id = reader.GetInt64(0);
            string description = reader.GetString(1);
            
            var hasDueDate = !reader.IsDBNull(2);
            DateTimeOffset? dueDate = hasDueDate ? reader.GetDateTimeOffset(2) : null;
            
            bool finished = reader.GetInt64(3) != 0;
            
            var result = new TodoItem(id, description, dueDate, finished);
            yield return result;
        }
            
        reader.Close();
    }

    private async Task<long> GetLastInsertRowId()
    {
        const string sql = "SELECT last_insert_rowid()";
        await using var command = new SqliteCommand(sql, _connection);

        var rowId = (long?)(await command.ExecuteScalarAsync());
        if (rowId == null)
        {
            throw new Exception("Cannot get the last insert row id.");
        }
        return rowId.Value;
    }

    public async Task<long> AddItemAsync(TodoItem model)
    {
        const string sql =
            "INSERT INTO todotable(description, duedate, isfinished) VALUES (@description, @duedate, @isfinished)";
        await using var command = new SqliteCommand(sql, _connection);
        command.Parameters.AddWithValue("@description", model.Description);
        command.Parameters.AddWithValue("@duedate", model.DueDate ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@isfinished", model.IsFinished);

        await command.ExecuteNonQueryAsync();
        
        return await GetLastInsertRowId();
    }

    public async Task DeleteItemAsync(long id)
    {
        const string sql = "DELETE FROM todotable WHERE id = @id";
        await using var command = new SqliteCommand(sql, _connection);
        command.Parameters.AddWithValue("@id", id);
        
        await command.ExecuteNonQueryAsync();
    }

    // https://stackoverflow.com/questions/73068065/c-sharp-way-to-pass-property-of-the-generic-type-like-an-enum
    public async Task UpdateItemAsync<T>(long id, Expression<Func<TodoItem, T>> property, T value)
    {
        if (property.Body is MemberExpression memberExpression)
        {
            var columnName = memberExpression.Member.Name.ToLower();
            
            string sql = $"UPDATE todotable SET {columnName} = @value WHERE id = @id";
            await using var command = new SqliteCommand(sql, _connection);

            command.Parameters.AddWithValue("@value", value);
            command.Parameters.AddWithValue("@id", id);
            await command.ExecuteNonQueryAsync();
        }
        else
        {
            throw new NotSupportedException();
        }
    }

    #endregion

    public void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
    }
}