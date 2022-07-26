using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Todo.Models;
using Todo.Services;

namespace Todo.ViewModels;

public class TodoListViewModel : TodoListViewModelBase
{
    #region Fields
    
    private readonly ReadOnlyObservableCollection<TodoItemViewModel> _items;

    #endregion

    #region Properties
    
    public ReadOnlyObservableCollection<TodoItemViewModel> Items => _items;

    #endregion
    
    public TodoListViewModel(in ITodoDatabase db, ref SourceList<TodoItemViewModel> itemSource) : base(db,
        ref itemSource)
    {
        var disposable = ItemSource
            .Connect()
            .AutoRefresh(p => p.IsFinished)
            .AutoRefresh(p => p.DueDate)
            .Sort(SortExpressionComparer<TodoItemViewModel>
                .Ascending(p => p.IsFinished)
                .ThenByAscending(p => p.DueDate ?? DateTimeOffset.MaxValue))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _items)
            .Subscribe();
    }

    #region Commands and Functions

    public async void AddTodoItem()
    {
        // At first creation, set the row id as -1 so that it is not assigned to the databse.
        var tempModel = new TodoItem(-1, string.Empty);
        long rowId = await Database.AddItemAsync(tempModel);

        var vm = new TodoItemViewModel(tempModel with { Id = rowId }, TodoItemViewModel.CreaterType.UserInput, Database);
        ItemSource.Add(vm);
    }

    public async void DeleteTodoItem(TodoItemViewModel vm)
    {
        await Database.DeleteItemAsync(vm.Id);
        ItemSource.Remove(vm);
    }

    #endregion
}