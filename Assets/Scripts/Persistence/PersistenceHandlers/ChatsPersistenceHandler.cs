using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistence.Accessors;
using Persistence.PersistenceObjects;
using ScriptableObjects.Chat;
using UnityEditor;

namespace Persistence.PersistenceHandlers
{
    public class ChatsPersistenceHandler : PersistentMonoBehaviour
    {
        private Dictionary<string, ChatSession> _chats;
        protected override async void Awake()
        {
            base.Awake();
        }

        private void LoadChats()
        {
            var chatAssetsGuid = AssetDatabase.FindAssets($"t:{nameof(ChatSession)}");

            _chats = new Dictionary<string, ChatSession>();

            AssetDatabase.Refresh();
            
            foreach (var guid in chatAssetsGuid)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                
                var chatSession = AssetDatabase.LoadAssetAtPath<ChatSession>(path);

                if (string.IsNullOrEmpty(chatSession.Guid))
                {
                    chatSession.Guid = Guid.NewGuid().ToString();
                    EditorUtility.SetDirty(chatSession);
                }
                _chats.Add(chatSession.Guid, chatSession);
            }
            
            AssetDatabase.SaveAssets();
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