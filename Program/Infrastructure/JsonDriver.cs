using System.Text.Json;
using program.Infrastructure.Interfaces;

namespace program.Infrastructure;

public class JsonDriver : IPersistence
{
    public JsonDriver(string directoryRecording)
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
        string jsonString = JsonSerializer.Serialize(objectSave);

        var name = objectSave.GetType().Name.ToLower();
        await File.WriteAllTextAsync($"{this.GetRecordingPath()}/{name}s.json", jsonString);
    }

    public void SetRecordingPath(string directory)
    {
        throw new NotImplementedException();
    }
}