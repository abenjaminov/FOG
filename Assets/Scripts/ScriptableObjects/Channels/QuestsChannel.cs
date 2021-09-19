using System;
using Entity.NPCs;
using ScriptableObjects.Chat;
using ScriptableObjects.Quests;
using ScriptableObjects.Traits;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Quests Channel", menuName = "Channels/Quests Channel")]
    public class QuestsChannel : ScriptableObject
    {
        [SerializeField] private NpcChannel _npcChannel;
        [SerializeField] private PlayerTraits _playerTraits;
        
        public UnityAction<Quest> QuestCompleteEvent;
        public UnityAction<Quest> QuestActivatedEvent;
        public UnityAction<Quest> QuestPendingCompleteEvent;
        public UnityAction<Quest> QuestStateChangedEvent;

        private void OnEnable()
        {
            _npcChannel.ChatEndedEvent += ChatEndedEvent;
        }

        private void OnDisable()
        {
            _npcChannel.ChatEndedEvent -= ChatEndedEvent;
        }

        private void ChatEndedEvent(ChatNpc chatNpc, ChatSession chatSession, ChatDialogOptionAction reason)
        {
            if (reason == ChatDialogOptionAction.Accept)
            {
                if(chatSession.SessionType == ChatSessionType.AssignQuest)
                    AssignQuest(chatSession.AssociatedQuest);
                else if (chatSession.SessionType == ChatSessionType.CompleteQuest)
                    chatSession.AssociatedQuest.Complete();
            }
        }

        public void OnQuestCompleted(Quest completedQuest)
        {
            QuestCompleteEvent?.Invoke(completedQuest);
            
            OnQuestStateChanged(completedQuest);
        }
        
        public void OnQuestPendingComplete(Quest pendingQuest)
        {
            QuestPendingCompleteEvent?.Invoke(pendingQuest);
            
            OnQuestStateChanged(pendingQuest);
        }
        
        public void OnQuestAssigned(Quest questToAssign)
        {
            QuestActivatedEvent?.Invoke(questToAssign);

            OnQuestStateChanged(questToAssign);
        }

        public void AssignQuest(Quest questToAssign)
        {
            if (_playerTraits.Level < questToAssign.RequiredLevel) return;
            
            questToAssign.Activate();
        }

        private void OnQuestStateChanged(Quest quest)
        {
            QuestStateChangedEvent?.Invoke(quest);
        }
    }
}