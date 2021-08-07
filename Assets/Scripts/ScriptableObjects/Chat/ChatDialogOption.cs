using System;
using ScriptableObjects.Quests;
using UnityEngine;

namespace ScriptableObjects.Chat
{
    [Serializable]
    public class ChatDialogOption
    {
        public string Text;
        public ChatDialogOptionAction Action;
    }
}