// From https://github.com/AvaloniaUI/Avalonia/issues/6071#issuecomment-861574988

using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;

namespace Todo.Views.Behaviors;

/// <summary>
/// Bind the TextBox's Text Property with UpdateSourceTrigger=LostFocus
/// </summary>
public class LostFocusUpdateBindingBehavior : Behavior<TextBox>
{
    static LostFocusUpdateBindingBehavior()
    {
        TextProperty.Changed.Subscribe(e => 
            (e.Sender as LostFocusUpdateBindingBehavior)?.OnBindingValueChanged()
        );
    }
        

    public static readonly StyledProperty<string> TextProperty = AvaloniaProperty.Register<LostFocusUpdateBindingBehavior, string>(
        "Text", defaultBindingMode: BindingMode.TwoWay);

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    protected override void OnAttached()
    {
        Debug.Assert(AssociatedObject != null, nameof(AssociatedObject) + " != null");
        AssociatedObject.LostFocus += OnLostFocus;
        base.OnAttached();
    }

    protected override void OnDetaching()
    {
        Debug.Assert(AssociatedObject != null, nameof(AssociatedObject) + " != null");
        AssociatedObject.LostFocus -= OnLostFocus;
        base.OnDetaching();
    }
        
    private void OnLostFocus(object? sender, RoutedEventArgs e)
    {
        if (AssociatedObject != null)
            Text = AssociatedObject.Text;
    }
        
    private void OnBindingValueChanged()
    {
        if (AssociatedObject != null)
            AssociatedObject.Text = Text;
    }
}