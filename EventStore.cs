using System.IO;
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
        if (!File.Exists(_filePath)) return [];
        try
        {
            return JsonSerializer.Deserialize<List<CalendarEvent>>(File.ReadAllText(_filePath), JsonOptions)?
                .OrderBy(item => item.Date).ToList() ?? [];
        }
        catch (JsonException)
        {
            return [];
        }
    }

    public void Save(IEnumerable<CalendarEvent> events)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
        File.WriteAllText(_filePath, JsonSerializer.Serialize(events.OrderBy(item => item.Date), JsonOptions));
    }
}
