using ScriptableObjects.Chat;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Npc Channel", menuName = "Channels/Npc Channel", order = 4)]
    public class NpcChannel : ScriptableObject
    {
        public UnityAction<ChatSession> RequestChatStartEvent;

        public void OnRequestChatStart(ChatSession chatSession)
        {
            RequestChatStartEvent?.Invoke(chatSession);
        }
    }
}