using System;
using Entity.NPCs;
using ScriptableObjects.Channels;
using ScriptableObjects.Chat;
using UnityEngine;

namespace ScriptableObjects.Quests
{
    [CreateAssetMenu(fileName = "Talk to Npc quest", menuName = "Quest/Talk to Npc quest")]
    public class TalkToNpcQuest : Quest
    {
        [Header("Chat to NPC quest")]
        public ChatNpc NpcPrefab;
        [SerializeField] private NpcChannel _npcChannel;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (State == QuestState.PendingComplete)
            {
                
            }
            
            _completeOnSpot = true;
        }

        public override void Complete()
        {
            _npcChannel.ChatStartedEvent -= ChatStartedEvent;
            
            base.Complete();
        }

        public override void Activate()
        {
            _npcChannel.ChatStartedEvent += ChatStartedEvent;
            State = QuestState.PendingComplete;
            _questsChannel.OnQuestAssigned(this);
        }

        private void ChatStartedEvent(ChatNpc chatNpc, ChatSession chatSession)
        {
            if (chatNpc.NpcId != NpcPrefab.NpcId || 
                chatSession.AssociatedQuest.Id != Id) return;
            
            Complete();
        }
    }
}