using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace NativeCalendar;

public sealed class EventStore
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
    private readonly string _filePath;

    public EventStore()
    {
        _filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "NativeCalendar",
            "events.json");
    }

    public IReadOnlyList<CalendarEvent> Load()
    {
        if (!File.Exists(_filePath)) return new List<CalendarEvent>();
        try
        {
            return JsonSerializer.Deserialize<List<CalendarEvent>>(File.ReadAllText(_filePath), JsonOptions)?
                .OrderBy(item => item.Date).ToList() ?? new List<CalendarEvent>();
        }
        catch (JsonException)
        {
            return new List<CalendarEvent>();
        }
    }

    public void Save(IEnumerable<CalendarEvent> events)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
        File.WriteAllText(_filePath, JsonSerializer.Serialize(events.OrderBy(item => item.Date), JsonOptions));
    }
}

/*using System.IO;
using System.Text.Json;

namespace NativeCalendar;

public sealed class EventStore
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
    private readonly string _filePath;

    public EventStore()
    {
        _filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "NativeCalendar",
            "events.json");
    }

    public IReadOnlyList<CalendarEvent> Load()
    {
        if (!File.Exists(_filePath)) return Array.Empty<CalendarEvent>();
        try
        {
            return JsonSerializer.Deserialize<List<CalendarEvent>>(File.ReadAllText(_filePath), JsonOptions);
                .OrderBy(item => item.Date).ToList() : Array.Empty<CalendarEvent>();
        }
        catch (JsonException)
        {
            return Array.Empty<CalendarEvent>();
        }
    }

    public void Save(IEnumerable<CalendarEvent> events)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
        File.WriteAllText(_filePath, JsonSerializer.Serialize(events.OrderBy(item => item.Date), JsonOptions));
    }
}
*/