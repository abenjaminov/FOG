namespace Persistence.Accessors
{
    public interface IPersistenceModuleAccessor
    {
        T GetValue<T>(string key);
        void PersistData<T>(string key, T data);
        PersistantModuleTypes GetModuleType();

        void LoadModule();
        void CloseModule();
    }
}