# Todo.Avalonia
The Todo app built by Avalonia with improved designs and features, inspired by [the official Avalonia website tutorial](https://docs.avaloniaui.net/tutorials/todo-list-app)

<img width="752" alt="Main screenshot" src="https://user-images.githubusercontent.com/63503910/180939268-ead46126-c859-44c8-8237-1fdc14a1b084.png">

## Features

[Screenshot: scrollable sections with upcoming Todos]
![Working screenshot: scrollable sections](https://user-images.githubusercontent.com/63503910/180939386-40ff8574-cfb2-4260-9821-e0ccfa9a700f.gif)

The top dashboard notify the upcoming Todos. The app is divided by two sections and scrollable. The top header text shows the current header. Using [HeaderedScrollViewer](https://github.com/stripe2933/HeaderedScrollViewer)

[Screenshot: feature 'Mark as finished' with automatically sorted]
![Working screenshot: mark as finished with dynamic sorting](https://user-images.githubusercontent.com/63503910/180939817-40b4c3b8-9073-4f23-aa64-39ff26a85df6.gif)

The showing order automatically sorted with remaining due date. If the Todo marked as finished, it does not shown at the top upcoming dashboard. (use ReactiveUI dynamic list)

[Screenshot: editable description and due date in control itself]
![Working screenshot: editable description and due date in control itself](https://user-images.githubusercontent.com/63503910/180940342-e9c8b25f-cca5-42aa-9fc6-b0a48ec30534.gif)

Each Todo item can be updated with TextBox and DatePicker in the control itself (not using dialog).

[Screenshot: add, delete, and update Todo items with SQLite database]
![Working screenshot: add, delete, and update Todo items with SQLite database](https://user-images.githubusercontent.com/63503910/180940601-93d8a97a-bb52-44de-8825-bd47114dbfc1.gif)

Add, delete, and update Todo items with SQLite database: All user actions are saved automatically (unnecessary to click any save button).

## Description

I am new to the Avalonia Framework, and this project is a practice for understanding the framework.
I tried to follow MVVM rules as much as possible, and I think it is a good example program to practice various functions of ReactiveUI (DynamicList, ObservableProperty, etc.).
I tested the app on macOS, but I didn't check if it works on the rest of the operating system. The IDE used was JetBrains Rider, which was designed by dividing the Design Time Database and the actual database for rendering the app in Design Time and injected dependencies.
I hope this project can help other new Avalonia users.
