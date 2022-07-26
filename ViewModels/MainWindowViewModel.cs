using System;
using System.Linq;
using DynamicData;
using Todo.Models;
using Todo.Services;

namespace Todo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        private readonly ITodoDatabase _database;

        #endregion
        
        #region Properties

        public UpcomingTodoListViewModel UpcomingTodoList { get; }
        public TodoListViewModel TodoList { get; }

        #endregion

        #region Constructor

        public MainWindowViewModel(in ITodoDatabase database)
        {
            _database = database;
            
            var sourceList = new SourceList<TodoItemViewModel>();
            sourceList.AddRange(database.GetItems().Select(item =>
                new TodoItemViewModel(item, TodoItemViewModel.CreaterType.Database, _database))
            );

            UpcomingTodoList = new UpcomingTodoListViewModel(database, ref sourceList);
            TodoList = new TodoListViewModel(database, ref sourceList);
        }
        
        #endregion
    }
}