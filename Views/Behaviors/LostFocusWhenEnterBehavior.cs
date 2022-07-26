using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Xaml.Interactivity;

namespace Todo.Views.Behaviors;

/// <summary>
/// Make TextBox lost the focus when Enter key down.
/// </summary>
public class LostFocusWhenEnterBehavior : Behavior<TextBox>
{
    protected override void OnAttached()
    {
        Debug.Assert(AssociatedObject != null, nameof(AssociatedObject) + " != null");
        AssociatedObject.KeyDown += OnKeyDown;
        base.OnAttached();
    }

    protected override void OnDetaching()
    {
        Debug.Assert(AssociatedObject != null, nameof(AssociatedObject) + " != null");
        AssociatedObject.KeyDown -= OnKeyDown;
        base.OnDetaching();
    }
        
    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (AssociatedObject != null && e.Key == Key.Enter)
        {
            var current = FocusManager.Instance?.Current;
            if (current != null)
            {
                var next = KeyboardNavigationHandler.GetNext(current, NavigationDirection.Next);
                if (next != null)
                {
                    FocusManager.Instance?.Focus(next, NavigationMethod.Directional);
                }
            }
        }
    }
}