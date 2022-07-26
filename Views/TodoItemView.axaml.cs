using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Todo.ViewModels;

namespace Todo.Views;

public partial class TodoItemView : UserControl
{
    public TodoItemView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <summary>
    /// Focus the description TextBox when user add a new TodoItem
    /// </summary>
    private void DescriptionTextBox_OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        if (sender is TextBox { DataContext: TodoItemViewModel { ViewModelCreator: TodoItemViewModel.CreaterType.UserInput } } textBox)
        {
            textBox.Focus();
        }
    }

    // private void UnselectDueDatePickerButton_Click(object? sender, RoutedEventArgs e)
    // {
    //     this.DueDatePicker.SetValue(DatePicker.SelectedDateProperty, null);
    //     e.Handled = true;
    // }
}