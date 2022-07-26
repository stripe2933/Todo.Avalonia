using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Todo.Views;

public partial class UpcomingTodoItemView : UserControl
{
    public UpcomingTodoItemView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}