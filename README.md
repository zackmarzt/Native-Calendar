# Native Calendar

A simple native calendar application for Windows, developed in C# using the WPF (Windows Presentation Foundation) framework and .NET 10.0.

## 📌 About the Project

**Native Calendar** allows viewing a monthly calendar, selecting specific days, and managing events (adding and removing). All created events are stored locally on the machine, ensuring that information persists even after the application is closed.

The project follows the **MVVM** (Model-View-ViewModel) architectural pattern, which keeps a clean separation between the user interface (UI) and business logic.

## 🚀 Features

- **Monthly Navigation**: Move forward or backward between months, or quickly return to the current month.
- **Day Selection**: Click a day to view events associated with it.
- **Event Management**: Add new events to a selected date or remove existing events.
- **Local Storage**: Your events are automatically saved and loaded using a file-based storage system (`EventStore.cs`).
- **Responsive Interface**: Status feedback to indicate when events are saved or removed.

## 📂 Project Structure (MVVM Pattern)

- **`MainWindow.xaml` / `MainWindow.xaml.cs`**: The main view where the calendar UI is declared.
- **`CalendarViewModel.cs`**: The primary ViewModel that manages UI state, the calendar day list, and actions to add/remove events.
- **`CalendarDay.cs` & `CalendarEvent.cs`**: Models that represent the properties of a day and an event, respectively.
- **`RelayCommand.cs`**: An `ICommand` implementation to bind UI button actions to ViewModel methods.
- **`EventStore.cs`**: Class responsible for data persistence. Loads and saves the event list to disk.

## ⚙️ How to Run

To run this project you will need **.NET SDK 10.0** installed.

1. Open a terminal in the project root folder (`c:\Users\zack\Documents\Native-Calendar`).
2. Run the command to build the project:
   ```bash
   dotnet build
   ```
3. Run the command to execute the application:
   ```bash
   dotnet run
   ```

---
*Project created for personal organization purposes and studying WPF/C#.*
