using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Channels;
using ScriptableObjects.Chat;
using ScriptableObjects.Quests;
using ScriptableObjects.Traits;
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
        [SerializeField] private QuestsChannel _questChannel;
        [SerializeField] private PersistenceChannel _persistenceChannel;
        [SerializeField] private PlayerTraits _playerTraits;
        [SerializeField] public List<ChatSession> ChatSessions;

        [SerializeField] public List<string> GeneralTextLines;
        

        private void Awake()
        {
            var visuals = Instantiate(_npcVisuals, Vector3.zero, Quaternion.Euler(0, faceLeft ? 180 : 0, 0), this.transform);
            visuals.transform.localPosition = Vector3.zero;
            _nameText.SetText(_name);
            
            if(GeneralTextLines.Count == 0)
                GeneralTextLines.Add("Hello there!");
            
            _questChannel.QuestActivatedEvent += QuestStateChanged;
            _questChannel.QuestCompleteEvent += QuestStateChanged;
            
            UpdateContentIndicator();
        }

        private void QuestStateChanged(Quest arg0)
        {
            UpdateContentIndicator();
        }

        private void UpdateContentIndicator()
        {
            var count = GetAvailableChatSessions().Count;

            _contentIndicator.SetActive(count > 0);
        }

        public void HandleDoubleClick()
        {
            _npcChannel.OnRequestChatStart(this);
        }

        public List<ChatSession> GetAvailableChatSessions()
        {
            return ChatSessions.Where(x => x.SessionType == ChatSessionType.Casual || 
                                           (x.AssociatedQuest.RequiredLevel <= _playerTraits.Level &&
                                            ((x.SessionType == ChatSessionType.AssignQuest &&
                                              x.AssociatedQuest.State == QuestState.PendingActive) ||
                                             (x.SessionType == ChatSessionType.CompleteQuest &&
                                              x.AssociatedQuest.State == QuestState.PendingComplete)))).ToList();
        }
    }
}