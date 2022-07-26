using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Todo.Services;

namespace Todo.ViewModels;

public class UpcomingTodoListViewModel : TodoListViewModelBase
{
    #region Fields
    
    private readonly ReadOnlyObservableCollection<TodoItemViewModel> _upcomingItems;

    #endregion

    #region Properties
    
    public ReadOnlyObservableCollection<TodoItemViewModel> UpcomingItems => _upcomingItems;

    #endregion
    
    #region Constructor

    public UpcomingTodoListViewModel(in ITodoDatabase db, ref SourceList<TodoItemViewModel> itemSource) : base(db, ref itemSource)
    {
        var disposable = ItemSource
            .Connect()
            .AutoRefresh(p => p.IsFinished)
            .AutoRefresh(p => p.DueDate)
            .Filter(p =>
                !p.IsFinished && p.DueDate.HasValue && p.DueDate <= DateTimeOffset.Now.AddDays(3)) // upcoming for 3 days
            .Sort(SortExpressionComparer<TodoItemViewModel>.Ascending(p => p.DueDate ?? DateTimeOffset.MaxValue))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _upcomingItems)
            .Subscribe();
    }

    #endregion
    
    #region Commands and Functions

    public void MarkAsFinish(TodoItemViewModel vm)
    {
        vm.IsFinished = true;
    }

    #endregion
}