using System;
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
        [SerializeField] bool faceLeft;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private string _name;
        [SerializeField] private GameObject _contentIndicator;
        
        public string NpcId;
        [SerializeField] private NpcChannel _npcChannel;
        [SerializeField] public List<ChatSession> ChatSessions;

        [SerializeField] public List<string> GeneralTextLines;
        

        private void Awake()
        {
            var visuals = Instantiate(_npcVisuals, Vector3.zero, Quaternion.Euler(0, faceLeft ? 180 : 0, 0), this.transform);
            visuals.transform.localPosition = Vector3.zero;
            _nameText.SetText(_name);
            
            if(GeneralTextLines.Count == 0)
                GeneralTextLines.Add("Hello there!");
        }
        
        public void HandleDoubleClick()
        {
            _npcChannel.OnRequestChatStart(this);
        }
    }
}