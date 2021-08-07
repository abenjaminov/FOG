using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Chat
{
    [Serializable]
    public class ChatItem
    {
        [TextArea(3,10)]
        public string Text;
        public List<ChatDialogOption> Options;
    }
}