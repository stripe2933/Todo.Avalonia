using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Todo.Views;

public partial class UpcomingTodoListView : UserControl
{
    public UpcomingTodoListView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}