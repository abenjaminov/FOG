using Persistence.Accessors;

namespace Persistence
{
    public interface IPersistentObject
    {
        PersistantModuleTypes GetModuleType();
        void OnModuleLoaded(IPersistenceModuleAccessor accessor);
        void OnModuleClosing(IPersistenceModuleAccessor accessor);
    }
}