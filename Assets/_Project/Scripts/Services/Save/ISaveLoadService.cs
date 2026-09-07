namespace _Project.Scripts.Services.Save
{
    public interface ISaveLoadService
    {
        bool Save<T>(string key, T data);
        bool TryLoad<T>(string key, out T data);
        bool Delete(string key);
    }
}
