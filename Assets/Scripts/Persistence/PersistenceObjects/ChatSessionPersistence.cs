using System;

namespace Persistence.PersistenceObjects
{
    [Serializable]
    public class ChatSessionPersistence
    {
        public string Guid;
        public bool IsOneTimeDone;
    }
}