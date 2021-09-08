using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScriptableObjects.Quests
{
    [CreateAssetMenu(fileName = "Quests List", menuName = "Game Configuration/Quests")]
    public class QuestsList : ScriptableObject
    {
        [Header("Tutorial")]
        [SerializeField] private List<Quest> TutorialQuests;
        
        [SerializeField] private List<Quest> Quests;

        public List<Quest> GetAllRunningQuests()
        {
            return Quests.Where(x => x.State != QuestState.PendingActive).ToList();
        }

        public void ResetQuests()
        {
            foreach (var quest in Quests)
            {
                quest.State = QuestState.PendingActive;
            }
            
            foreach (var quest in TutorialQuests)
            {
                quest.State = QuestState.PendingActive;
            }
        }
    }
}