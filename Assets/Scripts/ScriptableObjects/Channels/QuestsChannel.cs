using ScriptableObjects.Quests;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Quests Channel", menuName = "Channels/Quests Channel")]
    public class QuestsChannel : ScriptableObject
    {
        public UnityAction<Quest> QuestCompletedEvent;
        public UnityAction<Quest> QuestActiveEvent;

        public void OnQuestCompleted(Quest completedQuest)
        {
            QuestCompletedEvent?.Invoke(completedQuest);
        }
        
        public void OnQuestActive(Quest activeQuest)
        {
            QuestActiveEvent?.Invoke(activeQuest);
        }

        public void AssignQuest(Quest questToAssign)
        {
            OnQuestActive(questToAssign);
        }
    }
}