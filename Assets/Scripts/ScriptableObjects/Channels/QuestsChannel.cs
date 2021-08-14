using System;
using Entity.NPCs;
using ScriptableObjects.Chat;
using ScriptableObjects.Quests;
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
                    this.AssignQuest(chatSession.AssociatedQuest);
                else if (chatSession.SessionType == ChatSessionType.CompleteQuest)
                    this.CompleteQuest(chatSession.AssociatedQuest);
            }
        }

        public void CompleteQuest(Quest completedQuest)
        {
            QuestCompleteEvent?.Invoke(completedQuest);
        }
        
        public void AssignQuest(Quest questToAssign)
        {
            if (_playerTraits.Level < questToAssign.RequiredLevel) return;
            
            QuestActivatedEvent?.Invoke(questToAssign);
        }
    }
}