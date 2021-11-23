using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using Persistence.Accessors;
using Persistence.PersistenceObjects;
using ScriptableObjects.Chat;
using ScriptableObjects.GameConfiguration;
using UnityEngine;

namespace Persistence.PersistenceHandlers
{
    public class ChatsPersistenceHandler : PersistentMonoBehaviour
    {
        [SerializeField] private ChatsList _chatsList;
        private Dictionary<string, ChatSession> _chats;

        protected override async void Awake()
        {
            base.Awake();
        }

        private void LoadChats()
        {

            _chats = _chatsList.Chats.ToDictionary(x => x.Guid, x => x);
        }
        
        public override void OnModuleLoaded(IPersistenceModuleAccessor accessor)
        {
            LoadChats();

            var persistentChats = accessor.GetValue<List<ChatSessionPersistence>>("IsOneTime Chats");

            if (persistentChats == null) return;

            foreach (var chat in persistentChats)
            {
                if (!_chats.ContainsKey(chat.Guid)) continue;

                _chats[chat.Guid].IsOneTimeDone = chat.IsOneTimeDone;
            }
        }

        public override void OnModuleClosing(IPersistenceModuleAccessor accessor)
        {
            var chatsToPersist = _chats.Values.Where(x => x.IsOneTime).
                Select(c => new ChatSessionPersistence()
            {
                Guid = c.Guid ?? Guid.NewGuid().ToString(),
                IsOneTimeDone = c.IsOneTimeDone
            }).ToList();
            
            accessor.PersistData("IsOneTime Chats", chatsToPersist);
        }

        public override void PrintPersistantData()
        {
           
        }
    }
}