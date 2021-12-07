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

        public override void SetState(QuestState newState)
        {
            base.SetState(newState);

            _completeOnSpot = true;
        }

        public override void Complete()
        {
            _npcChannel.ChatEndedEvent -= ChatEndedEvent;
            
            base.Complete();
        }

        public override void Activate()
        {
            _npcChannel.ChatEndedEvent += ChatEndedEvent;
            State = QuestState.PendingComplete;
            _questsChannel.OnQuestAssigned(this);
        }

        private void ChatEndedEvent(ChatNpc chatNpc, ChatSession session, ChatDialogOptionAction reason)
        {
            if (reason != ChatDialogOptionAction.Accept) return;
            
            if (chatNpc.NpcId != NpcPrefab.NpcId || 
                session.AssociatedQuest.Id != Id) return;
            
            Complete();
        }
    }
}