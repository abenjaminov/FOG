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
        public ChatNpc NpcPrefab;
        [SerializeField] private NpcChannel _npcChannel;

        protected override void QuestCompleted()
        {
            _npcChannel.RequestChatStartEvent -= RequestChatStartEvent;
        }

        protected override void QuestActive()
        {
            _npcChannel.RequestChatStartEvent += RequestChatStartEvent;
        }

        private void RequestChatStartEvent(ChatNpc chatNpc)
        {
            if (chatNpc.NpcId == NpcPrefab.NpcId)
            {
                _questsChannel.OnQuestCompleted(this);
            }
        }
    }
}