using System;

namespace Persistence.PersistenceObjects.Quests
{
    [Serializable]
    public class KillEnemiesQuestPersistence : QuestPersistence
    {
        public int ActualEnemiesKilled;
    }
}