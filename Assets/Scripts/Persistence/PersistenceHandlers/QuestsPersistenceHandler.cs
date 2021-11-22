using System.Linq;
using Persistence.Accessors;
using Persistence.PersistenceObjects.Quests;
using ScriptableObjects.GameConfiguration;
using ScriptableObjects.Quests;
using UnityEngine;

namespace Persistence.PersistenceHandlers
{
    public class QuestsPersistenceHandler : PersistentMonoBehaviour
    {
        [SerializeField] private QuestsList _questsList;
        public override void OnModuleLoaded(IPersistenceModuleAccessor accessor)
        {
            
        }

        public override void OnModuleClosing(IPersistenceModuleAccessor accessor)
        {
            var quests = _questsList.GetAllRunningQuests();
            var killEnemiesQuests = quests.Where(x => x is KillEnemiesQuest).Select(
                x => new KillEnemiesQuestPersistence()
                {
                    Id = x.Id,
                    State = x.State,
                    ActualEnemiesKilled = ((KillEnemiesQuest) x).ActualEnemiesKilled
                });
            
            accessor.PersistData("RunningKillEnemiesQuests", killEnemiesQuests);
            
            var collectItemsQuests = quests.Where(x => x is CollectItemsQuest).Select(
                x => new QuestPersistence()
                {
                    Id = x.Id,
                    State = x.State,
                });
            
            accessor.PersistData("RunningCollectItemsQuests", collectItemsQuests);

            var noProgressQuests = quests.Where(x => !(x is KillEnemiesQuest) && !(x is CollectItemsQuest)).Select(
                x => new QuestPersistence()
                {
                    Id = x.Id,
                    State = x.State
                });
            accessor.PersistData("RunningNoProgressQuests", noProgressQuests);
        }

        public override void PrintPersistantData()
        {
            
        }
    }
}