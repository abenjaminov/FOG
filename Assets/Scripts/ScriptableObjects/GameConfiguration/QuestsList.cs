using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Quests;
using UnityEngine;

namespace ScriptableObjects.GameConfiguration
{
    [CreateAssetMenu(fileName = "Quests List", menuName = "Game Configuration/Quests")]
    public class QuestsList : ScriptableObject
    {
        [Header("Tutorial")]
        [SerializeField] private List<Quest> TutorialQuests;
        
        [SerializeField] private List<Quest> Karf;
        
        [SerializeField] private List<Quest> Map5;
        [SerializeField] private List<Quest> Crossroads;
        
        [SerializeField] private List<Quest> Quests;

        public List<Quest> GetAllRunningQuests()
        {
            var runningQuests = new List<Quest>();
            
            runningQuests.AddRange(Quests.Where(x => x.State == QuestState.Active || x.State == QuestState.PendingComplete));
            runningQuests.AddRange(TutorialQuests.Where(x => x.State == QuestState.Active || x.State == QuestState.PendingComplete));
            runningQuests.AddRange(Karf.Where(x => x.State == QuestState.Active || x.State == QuestState.PendingComplete));
            runningQuests.AddRange(Map5.Where(x => x.State == QuestState.Active || x.State == QuestState.PendingComplete));
            runningQuests.AddRange(Crossroads.Where(x => x.State == QuestState.Active || x.State == QuestState.PendingComplete));

            return runningQuests;
        }

        public void ResetQuests()
        {
            foreach (var quest in Quests)
            {
                quest.ResetQuest();
            }
            
            foreach (var quest in TutorialQuests)
            {
                quest.ResetQuest();
            }
            
            foreach (var quest in Karf)
            {
                quest.ResetQuest();
            }
            
            foreach (var quest in Map5)
            {
                quest.ResetQuest();
            }
            
            foreach (var quest in Crossroads)
            {
                quest.ResetQuest();
            }
        }
    }
}