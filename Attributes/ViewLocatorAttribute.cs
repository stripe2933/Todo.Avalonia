using System;

namespace Todo.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ViewLocatorAttribute : Attribute
{
    public string ViewName { get; }
    
    public ViewLocatorAttribute(string viewName)
    {
        ViewName = viewName;
    }
}