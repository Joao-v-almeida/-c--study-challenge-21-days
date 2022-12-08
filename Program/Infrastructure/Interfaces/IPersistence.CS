namespace program.Infrastructure.Interfaces;
public interface IPersistence
{
    Task Save(object objectSave);

    void Delete(object objectDelete);

    void Change(string Id, object objectChange);

    List<Object> FindId(string Id);

    List<Object> FindAll();

    string GetRecordingPath();
}