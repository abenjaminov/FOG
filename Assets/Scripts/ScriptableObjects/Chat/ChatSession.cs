using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Chat
{
    [CreateAssetMenu(fileName = "Chat Session", menuName = "Chat/Chat session", order = 0)]
    public class ChatSession : ScriptableObject
    {
        public List<ChatItem> ChatItems;
    }
}