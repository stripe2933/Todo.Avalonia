using System;

namespace Todo.Models;

/// <summary>
/// A model representing To-do item
/// </summary>
/// <param name="Id">An unique id for each To-do item (the primary key for SQLite database)</param>
/// <param name="Description">A description</param>
/// <param name="DueDate">A due date of the To-do item, null for unspecified</param>
/// <param name="IsFinished">Is to-do marked as finished?</param>
public record TodoItem(long Id, string Description, DateTimeOffset? DueDate = null, bool IsFinished = false);