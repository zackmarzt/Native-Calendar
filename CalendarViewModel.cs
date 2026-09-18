using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace NativeCalendar;

public sealed class CalendarViewModel : INotifyPropertyChanged
{
    private readonly EventStore _store = new();
    private readonly List<CalendarEvent> _events;
    private DateTime _displayedMonth = new(DateTime.Today.Year, DateTime.Today.Month, 1);
    private DateTime _selectedDate = DateTime.Today;
    private string _newEventTitle = string.Empty;
    private string _statusMessage = "Selecione um dia para criar um evento.";

    public CalendarViewModel()
    {
        _events = _store.Load().ToList();
        PreviousMonthCommand = new RelayCommand(() => { _displayedMonth = _displayedMonth.AddMonths(-1); RefreshCalendar(); });
        NextMonthCommand = new RelayCommand(() => { _displayedMonth = _displayedMonth.AddMonths(1); RefreshCalendar(); });
        TodayCommand = new RelayCommand(() => { _displayedMonth = new(DateTime.Today.Year, DateTime.Today.Month, 1); _selectedDate = DateTime.Today; RefreshCalendar(); });
        SelectDayCommand = new RelayCommand<CalendarDay>(day => { if (day is not null) { _selectedDate = day.Date; RefreshCalendar(); } });
        AddEventCommand = new RelayCommand(AddEvent, () => !string.IsNullOrWhiteSpace(NewEventTitle));
        RemoveEventCommand = new RelayCommand<CalendarEvent>(RemoveEvent);
        RefreshCalendar();
    }

    public ObservableCollection<CalendarDay> Days { get; } = [];
    public ObservableCollection<CalendarEvent> SelectedDayEvents { get; } = [];
    public ICommand PreviousMonthCommand { get; }
    public ICommand NextMonthCommand { get; }
    public ICommand TodayCommand { get; }
    public ICommand SelectDayCommand { get; }
    public RelayCommand AddEventCommand { get; }
    public ICommand RemoveEventCommand { get; }
    public string MonthTitle => _displayedMonth.ToString("MMMM yyyy", new System.Globalization.CultureInfo("pt-BR"));
    public string SelectedDateTitle => _selectedDate.ToString("dddd, d 'de' MMMM", new System.Globalization.CultureInfo("pt-BR"));

    public string NewEventTitle
    {
        get => _newEventTitle;
        set { _newEventTitle = value; OnPropertyChanged(); AddEventCommand.RaiseCanExecuteChanged(); }
    }

    public string StatusMessage { get => _statusMessage; private set { _statusMessage = value; OnPropertyChanged(); } }
    public event PropertyChangedEventHandler? PropertyChanged;

    private void RefreshCalendar()
    {
        Days.Clear();
        var firstGridDate = _displayedMonth.AddDays(-(int)_displayedMonth.DayOfWeek); // Sunday is 0, so this will start the grid on Sunday
        for (var index = 0; index < 42; index++)
        {
            var date = firstGridDate.AddDays(index);
            Days.Add(new CalendarDay(date, date.Month == _displayedMonth.Month, date.Date == _selectedDate.Date, _events.Any(item => item.Date.Date == date.Date)));
        }
        SelectedDayEvents.Clear();
        foreach (var item in _events.Where(item => item.Date.Date == _selectedDate.Date)) SelectedDayEvents.Add(item);
        OnPropertyChanged(nameof(MonthTitle));
        OnPropertyChanged(nameof(SelectedDateTitle));
    }

    private void AddEvent()
    {
        var title = NewEventTitle.Trim();
        if (title.Length == 0) return;
        _events.Add(new CalendarEvent(Guid.NewGuid(), title, _selectedDate));
        Persist("Evento salvo localmente.");
        NewEventTitle = string.Empty;
        RefreshCalendar();
    }

    private void RemoveEvent(CalendarEvent? item)
    {
        if (item is null) return;
        _events.Remove(item);
        Persist("Evento removido.");
        RefreshCalendar();
    }

    private void Persist(string successMessage)
    {
        try { _store.Save(_events); StatusMessage = successMessage; }
        catch (IOException) { StatusMessage = "Não foi possível salvar os eventos localmente."; }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new(propertyName));
}
