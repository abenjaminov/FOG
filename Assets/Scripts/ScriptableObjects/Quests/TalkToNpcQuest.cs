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

        protected override void QuestCompleted()
        {
            _npcChannel.ChatStartedEvent -= ChatStartedEvent;
        }

        protected override void QuestActive()
        {
            _npcChannel.ChatStartedEvent += ChatStartedEvent;
        }

        private void ChatStartedEvent(ChatNpc chatNpc, ChatSession chatSession)
        {
            if (chatNpc.NpcId != NpcPrefab.NpcId || 
                chatSession.AssociatedQuest.Id != Id) return;
            
            Complete();
        }
    }
}