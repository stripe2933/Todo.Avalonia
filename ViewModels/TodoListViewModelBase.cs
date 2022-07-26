using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Todo.Models;
using Todo.Services;

namespace Todo.ViewModels;

public abstract class TodoListViewModelBase : ViewModelBase
{
    #region Fields

    protected readonly ITodoDatabase Database;
    protected readonly SourceList<TodoItemViewModel> ItemSource;

    #endregion

    #region Constructor

    protected TodoListViewModelBase(in ITodoDatabase db, ref SourceList<TodoItemViewModel> itemSource)
    {
        Database = db;
        ItemSource = itemSource;
    }

    #endregion
}