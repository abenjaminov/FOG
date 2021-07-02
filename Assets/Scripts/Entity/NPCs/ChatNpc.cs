﻿using System;
using System.Collections.Generic;
using ScriptableObjects.Channels;
using ScriptableObjects.Chat;
using TMPro;
using UI.Mouse;
using UnityEngine;

namespace Entity.NPCs
{
    public class ChatNpc : MonoBehaviour, IDoubleClickHandler
    {
        [Header("Visuals")] [SerializeField] private GameObject _npcVisuals;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private string _name;
        
        [SerializeField] private NpcChannel _npcChannel;
        [SerializeField] private List<ChatSession> ChatSessions;

        private void Awake()
        {
            var visuals = Instantiate(_npcVisuals, Vector3.zero, Quaternion.identity, this.transform);
            visuals.transform.localPosition = Vector3.zero;
            _nameText.SetText(_name);
        }
        
        public void HandleDoubleClick()
        {
            _npcChannel.OnRequestChatStart(ChatSessions[0]);
        }
    }
}