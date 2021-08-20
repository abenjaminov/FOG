using System;
using ScriptableObjects.Quests;

namespace Persistence.PersistenceObjects.Quests
{
    [Serializable]
    public class QuestPersistence
    {
        public string Id;
        public QuestState State;
    }
}