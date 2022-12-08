using System.ComponentModel;
using System.Text.Json;
using program.Infrastructure.Interfaces;

namespace program.Infrastructure;

public class CsvDriver : IPersistence
{
    public CsvDriver(string directoryRecording)
    {
        this.directoryRecording = directoryRecording;
    }

    private string directoryRecording = "";

    public string GetRecordingPath()
    {
        return this.directoryRecording;
    }

    public void Change(string Id, object objectChange)
    {
        throw new NotImplementedException();
    }

    public void Delete(object objectDelete)
    {
        throw new NotImplementedException();
    }

    public List<object> FindAll()
    {
        throw new NotImplementedException();
    }

    public List<object> FindId(string Id)
    {
        throw new NotImplementedException();
    }

    public async Task Save(object objectSave)
    {
        var linesCsv = new List<string>();
        var property = TypeDescriptor.GetProperties(objectSave).OfType<PropertyDescriptor>();
        var header = string.Join(";", property.ToList().Select(x => x.Name));

        linesCsv.Add(header);

        var list = new List<object>();
        list.Add(objectSave);

        var valueLines = list.Select(row => string.Join(";", header.Split(';').Select(a => row.GetType()?.GetProperty(a)?.GetValue(row, null))));
        linesCsv.AddRange(valueLines);

        var csvString = string.Empty;
        foreach (var line in linesCsv)
        {
            csvString += line + "\n";
        }

        var name = objectSave.GetType().Name.ToLower();
        await File.WriteAllTextAsync($"{this.GetRecordingPath()}/{name}s.csv", csvString);
    }

    public void SetRecordingPath(string directory)
    {
        throw new NotImplementedException();
    }
}