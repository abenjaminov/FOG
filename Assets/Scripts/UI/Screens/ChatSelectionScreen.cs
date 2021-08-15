using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Assets.HeroEditor.Common.CommonScripts;
using Entity.NPCs;
using ScriptableObjects.Chat;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace UI.Screens
{
    public class ChatSelectionScreen : MonoBehaviour
    {
        private ChatNpc _chatNpc;
        [SerializeField] private ChatScreen _chatScreen;
        [SerializeField] private TextMeshProUGUI _titleText;

        [SerializeField] private ChatSelectionItem _chatSelectionItemPrefab;
        [SerializeField] private GameObject _selectionOptionsPanel;

        private List<ChatSelectionItem> selectionItems;

        public void SelectChat(ChatNpc chatNpc, List<ChatSession> sessions)
        {
            _chatNpc = chatNpc;
            SetTitle(chatNpc);
            
            selectionItems = new List<ChatSelectionItem>();

            for (int i = 0; i < sessions.Count; i++)
            {
                var session = sessions[i];
                var selectionItem = Instantiate(_chatSelectionItemPrefab, _selectionOptionsPanel.transform);
                selectionItem.transform.localPosition = new Vector3(20, -10 - (20 * i), 0);
                selectionItem.SetText(session.GetSessionName());
                selectionItem.Session = session;
                selectionItem.ChatSelectedEvent += ChatSelectedEvent;
                selectionItems.Add(selectionItem);
            }

            if (selectionItems.Count == 1)
            {
                ChatSelectedEvent(selectionItems[0]);
            }
        }

        private void SetTitle(ChatNpc chatNpc)
        {
            var index = Random.Range(0, chatNpc.GeneralTextLines.Count);
            var line = chatNpc.GeneralTextLines[index];

            _titleText.text = line;
        }
        
        private void ChatSelectedEvent(ChatSelectionItem chatItem)
        {
            _chatScreen.SetActive(true);
            _chatScreen.StartChat(_chatNpc, chatItem.Session);
            this.SetActive(false);
        }

        private void OnDisable()
        {
            foreach (var selectionItem in selectionItems)
            {
                selectionItem.ChatSelectedEvent -= ChatSelectedEvent;
                Destroy(selectionItem.gameObject);
            }
        }
    }
}