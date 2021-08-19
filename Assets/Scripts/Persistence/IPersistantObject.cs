using Persistence.Accessors;

namespace Persistence
{
    public interface IPersistantObject
    {
        PersistantModuleTypes GetModuleType();
        void OnModuleLoaded(IPersistenceModuleAccessor accessor);
        void OnModuleClosing(IPersistenceModuleAccessor accessor);
    }
}