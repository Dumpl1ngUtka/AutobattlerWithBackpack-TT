namespace Managers.SaveLoadManager
{
    public interface ISaveLoadManager<T>
    {
        public void Save(string path, T data);
        
        public T Load(string path);
    }
}