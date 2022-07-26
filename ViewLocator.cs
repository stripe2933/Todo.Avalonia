using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Todo.Attributes;
using Todo.ViewModels;

namespace Todo
{
    public class ViewLocator : IDataTemplate
    {
        public IControl Build(object data)
        {
            string name;

            // If ViewModel has ViewLocatorAttribute (specify the View name explicitly), use the name
            // Else, use convention (replacing "ViewModel" to "View")
            var viewLocatorAttributes = data.GetType().GetCustomAttributes(typeof(ViewLocatorAttribute), false);
            if (viewLocatorAttributes.Length > 0 && viewLocatorAttributes[0] is ViewLocatorAttribute attribute)
            {
                name = attribute.ViewName;
            }
            else
            {
                name = data.GetType().FullName!.Replace("ViewModel", "View");
            }
            
            var type = Type.GetType(name);
            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!; // Success to find the view
            }

            return new TextBlock { Text = "Not Found: " + name }; // Cannot find the View
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}