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

        public string GetSessionName()
        {
            if (SessionType == ChatSessionType.AssignQuest)
            {
                return AssociatedQuest.Name + " (Level " + AssociatedQuest.RequiredLevel + ")";
            }
            else if (SessionType == ChatSessionType.CompleteQuest)
            {
                return "Complete - " + AssociatedQuest.Name;
            }
            else if (SessionType == ChatSessionType.Casual)
            {
                return "Casual Chat"; // TODO : How is the name determined here?
            }

            return "";
        }
    }

    [Serializable]
    public enum ChatSessionType
    {
        AssignQuest,
        CompleteQuest,
        Casual
    }
}