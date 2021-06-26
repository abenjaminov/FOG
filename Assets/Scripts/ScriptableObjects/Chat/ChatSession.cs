using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Chat
{
    [CreateAssetMenu(fileName = "Chat Session", menuName = "Chat/Chat session", order = 0)]
    public class ChatSession : ScriptableObject
    {
        public List<ChatItem> ChatItems;
    }

    [Serializable]
    public class ChatItem
    {
        [TextArea(3,10)]
        public string Text;
        public List<ChatDialogOption> Options;
    }

    [Serializable]
    public class ChatDialogOption
    {
        public string Text;
        public ChatDialogOptionAction Action;
    }

    [Serializable]
    public enum ChatDialogOptionAction
    {
        Continue,
        Back,
        AssignQuest,
        Close
    }
}