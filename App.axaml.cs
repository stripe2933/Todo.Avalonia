using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Todo.Services;
using Todo.ViewModels;
using Todo.Views;

namespace Todo
{
    public partial class App : Application
    {
        private readonly ITodoDatabase _database;

        public App()
        {
            _database = new Database("database.sqlite");
        }
        

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

            AppDomain.CurrentDomain.ProcessExit += (_, _) => _database.Dispose();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(_database),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}