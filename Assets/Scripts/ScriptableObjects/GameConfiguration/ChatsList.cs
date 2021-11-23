using System;
using System.Collections.Generic;
using Helpers;
using ScriptableObjects.Chat;
using UnityEngine;

namespace ScriptableObjects.GameConfiguration
{
    [CreateAssetMenu(fileName = "Chats List", menuName = "Game Configuration/Chats")]
    public class ChatsList : ScriptableObject
    {
        public List<ChatSession> Chats;

        private void OnEnable()
        {
#if UNITY_EDITOR
            var chatAssets = AssetsHelper.GetAllAssets<ChatSession>();

            Chats ??= new List<ChatSession>();
            Chats.Clear();

            UnityEditor.AssetDatabase.Refresh();
            
            foreach (var chatSession in chatAssets)
            {
                if (string.IsNullOrEmpty(chatSession.Guid))
                {
                    chatSession.Guid = Guid.NewGuid().ToString();
                    UnityEditor.EditorUtility.SetDirty(chatSession);
                }
                Chats.Add(chatSession);
            }
            
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }
    }
}