using Todo.Services;

namespace Todo.ViewModels;

public static class DesignData
{
    public static MainWindowViewModel DesignTimeMainWindowViewModel { get; }
    public static UpcomingTodoListViewModel DesignTimeUpcomingTodoListViewModel { get; }
    public static TodoListViewModel DesignTimeTodoListViewModel { get; }
    public static TodoItemViewModel DesignTimeTodoItemViewModel { get; }

    static DesignData()
    {
        DesignTimeMainWindowViewModel = new MainWindowViewModel(new DesignTimeDatabase());
        DesignTimeUpcomingTodoListViewModel = DesignTimeMainWindowViewModel.UpcomingTodoList;
        DesignTimeTodoListViewModel = DesignTimeMainWindowViewModel.TodoList;
        DesignTimeTodoItemViewModel = DesignTimeTodoListViewModel.Items[^1];
    }
}