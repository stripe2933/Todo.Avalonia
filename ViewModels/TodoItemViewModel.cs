using System;
using System.Reactive.Linq;
using ReactiveUI;
using Todo.Models;
using Todo.Services;

namespace Todo.ViewModels;

public class TodoItemViewModel : ViewModelBase
{
    #region Enums

    public enum CreaterType
    {
        Database, // The TodoItem ViewModel is created by Database's models and do not need to be focused when VisualAttached
        UserInput // The TodoItem ViewModel is created by user input (descripted to be "New to-do") and should be focused when VisualAttached
    }

    #endregion
    
    #region Fields

    private readonly ITodoDatabase _database;

    private bool _isFinished;
    private string _description;
    private DateTimeOffset? _dueDate;
    
    private readonly ObservableAsPropertyHelper<bool> _isDueOver;

    #endregion

    #region Properties
    
    public long Id { get; }
    public string Description
    {
        get => _description;
        set
        {
            this.RaiseAndSetIfChanged(ref _description, value);
            _database.UpdateItemAsync(Id, x => x.Description, value).Wait();
        }
    }
    public DateTimeOffset? DueDate
    {
        get => _dueDate;
        set
        {
            this.RaiseAndSetIfChanged(ref _dueDate, value);
            _database.UpdateItemAsync(Id, x => x.DueDate, value).Wait();
        }
    }
    public bool IsFinished
    {
        get => _isFinished;
        set
        {
            this.RaiseAndSetIfChanged(ref _isFinished, value);
            _database.UpdateItemAsync(Id, x => x.IsFinished, value).Wait();
        }
    }

    public bool IsDueOver => _isDueOver.Value;
    public CreaterType ViewModelCreator { get; }

    #endregion

    #region Constructor

    public TodoItemViewModel(TodoItem model, CreaterType viewModelCreator, in ITodoDatabase database)
    {
        _database = database;
        
        Id = model.Id;
        _description = model.Description; // Assign by the field variables, not properties; assign by property executes SQLite update call
        _dueDate = model.DueDate;
        _isFinished = model.IsFinished;

        ViewModelCreator = viewModelCreator;

        _isDueOver = this.WhenAnyValue(x => x.IsFinished, x => x.DueDate)
            .Select(p => !p.Item1 && p.Item2 != null && p.Item2 <= DateTimeOffset.Now)
            .ToProperty(this, x => x.IsDueOver, out _isDueOver);
    }

    #endregion
}