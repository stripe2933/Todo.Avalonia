using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;

namespace Todo.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // Make Description TextBox lost focus when click outside the TextBox
            AddHandler(PointerPressedEvent, (_, _) =>
            {
                this.Focus();
            });
        }
    }
}