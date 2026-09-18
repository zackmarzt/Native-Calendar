namespace NativeCalendar;

public sealed class CalendarDay(DateTime date, bool isCurrentMonth, bool isSelected, bool hasEvents)
{
    public DateTime Date { get; } = date;
    public string DayNumber => Date.Day.ToString();
    public bool IsCurrentMonth { get; } = isCurrentMonth;
    public bool IsSelected { get; } = isSelected;
    public bool HasEvents { get; } = hasEvents;
}
