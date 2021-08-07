using System;
using System.Collections.Generic;
using ScriptableObjects.Quests;
using UnityEngine;

namespace ScriptableObjects.Chat
{
    [CreateAssetMenu(fileName = "Chat Session", menuName = "Chat/Chat session", order = 0)]
    public class ChatSession : ScriptableObject
    {
        public ChatSessionType SessionType;
        public Quest AssociatedQuest;
        public List<ChatItem> ChatItems;
    }

    [Serializable]
    public enum ChatSessionType
    {
        AssignQuest,
        CompleteQuest,
        Casual
    }
}