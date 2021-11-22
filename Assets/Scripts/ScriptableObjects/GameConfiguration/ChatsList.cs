using System.Collections.Generic;
using ScriptableObjects.Chat;
using UnityEngine;

namespace ScriptableObjects.GameConfiguration
{
    [CreateAssetMenu(fileName = "Chats List", menuName = "Game Configuration/Chats")]
    public class ChatsList : ScriptableObject
    {
        public List<ChatSession> Chats;
    }
}