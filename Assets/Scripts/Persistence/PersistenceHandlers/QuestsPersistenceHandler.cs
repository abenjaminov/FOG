using System.Collections.Generic;
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
            var killEnemiesQuest = accessor.GetValue<List<KillEnemiesQuestPersistence>>("RunningKillEnemiesQuests");
            var collectItemsQuests = accessor.GetValue<List<QuestPersistence>>("RunningCollectItemsQuests");
            var noProgressQuests = accessor.GetValue<List<QuestPersistence>>("RunningNoProgressQuests");

            if (killEnemiesQuest != null)
            {
                foreach (var quest in killEnemiesQuest)
                {
                    if (!_questsList.QuestsMap.ContainsKey(quest.Id)) continue;
                    
                    ((KillEnemiesQuest)_questsList.QuestsMap[quest.Id]).ActualEnemiesKilled = quest.ActualEnemiesKilled;
                    _questsList.QuestsMap[quest.Id].SetState(quest.State);
                }
            }

            if (collectItemsQuests != null)
            {
                foreach (var quest in collectItemsQuests)
                {
                    if (!_questsList.QuestsMap.ContainsKey(quest.Id)) continue;
                    _questsList.QuestsMap[quest.Id].SetState(quest.State);
                }
            }

            if (noProgressQuests != null)
            {
                foreach (var quest in noProgressQuests)
                {
                    if (!_questsList.QuestsMap.ContainsKey(quest.Id)) continue;
                    _questsList.QuestsMap[quest.Id].SetState(quest.State);
                }
            }
        }

        public override void OnModuleClosing(IPersistenceModuleAccessor accessor)
        {
            var quests = _questsList.GetAllActivatedQuests();
            var killEnemiesQuests = quests.Where(x => x is KillEnemiesQuest).Select(
                x => new KillEnemiesQuestPersistence()
                {
                    Id = x.Id,
                    State = x.State,
                    ActualEnemiesKilled = ((KillEnemiesQuest) x).ActualEnemiesKilled
                }).ToList();
            
            accessor.PersistData("RunningKillEnemiesQuests", killEnemiesQuests);
            
            var collectItemsQuests = quests.Where(x => x is CollectItemsQuest).Select(
                x => new QuestPersistence()
                {
                    Id = x.Id,
                    State = x.State,
                }).ToList();
            
            accessor.PersistData("RunningCollectItemsQuests", collectItemsQuests);

            var noProgressQuests = quests.Where(x => !(x is KillEnemiesQuest) && !(x is CollectItemsQuest)).Select(
                x => new QuestPersistence()
                {
                    Id = x.Id,
                    State = x.State
                }).ToList();
            accessor.PersistData("RunningNoProgressQuests", noProgressQuests);
        }

        public override void PrintPersistantData()
        {
            
        }
    }
}