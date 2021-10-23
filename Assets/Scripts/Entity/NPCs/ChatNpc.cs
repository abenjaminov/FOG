using System;
using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor.Common.CommonScripts;
using ScriptableObjects.Channels;
using ScriptableObjects.Chat;
using ScriptableObjects.Quests;
using ScriptableObjects.Traits;
using TMPro;
using UI.Mouse;
using UnityEngine;
using UnityEngine.UI;

namespace Entity.NPCs
{
    public class ChatNpc : MonoBehaviour, IDoubleClickHandler
    {
        [Header("Visuals")] [SerializeField] private GameObject _npcVisuals;
        [SerializeField] bool faceLeft;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private string _name;
        [SerializeField] private RawImage _indicator;
        [SerializeField] private Texture _completedIndicator;
        [SerializeField] private Texture _availableIndicator;
        
        public string NpcId;
        [SerializeField] private NpcChannel _npcChannel;
        [SerializeField] private QuestsChannel _questChannel;
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
            
            _questChannel.QuestStateChangedEvent += QuestStateChanged;
            _npcChannel.ChatEndedEvent += ChatEndedEvent;

            UpdateContentIndicator();
        }

        private void OnDestroy()
        {
            _questChannel.QuestStateChangedEvent -= QuestStateChanged;
            _npcChannel.ChatEndedEvent -= ChatEndedEvent;
        }

        private void ChatEndedEvent(ChatNpc arg0, ChatSession arg1, ChatDialogOptionAction arg2)
        {
            UpdateContentIndicator();
        }
        
        private void QuestStateChanged(Quest quest)
        {
            UpdateContentIndicator();
        }

        private void UpdateContentIndicator()
        {
            var chatSessions = GetAvailableChatSessions();

            var availableCount =
                chatSessions.Count(
                    x => x.AssociatedQuest != null && x.AssociatedQuest.State == QuestState.PendingActive);
            
            var completedCount =
                chatSessions.Count(
                    x => x.AssociatedQuest != null && x.AssociatedQuest.State == QuestState.Completed);

            _indicator.SetActive(chatSessions.Count > 0);

            if (availableCount > 0 && completedCount > 0)
            {
                
            }
            else if (availableCount > 0)
            {
                _indicator.texture = _availableIndicator;
            }
            else
            {
                _indicator.texture = _completedIndicator;
            }
        }

        public void HandleDoubleClick()
        {
            _npcChannel.OnRequestChatStart(this);
        }

        public List<ChatSession> GetAvailableChatSessions()
        {
            return ChatSessions.Where(x => (x.SessionType == ChatSessionType.Casual && (!x.IsOneTime || (x.IsOneTime && !x.IsOneTimeDone))) ||
                                           
                                           (x.AssociatedQuest != null && x.AssociatedQuest.RequiredLevel <= _playerTraits.Level &&
                                            
                                            (x.AssociatedQuest.DependencyQuest == null || 
                                             x.AssociatedQuest.DependencyQuest.State == QuestState.Completed) &&
                                            
                                            ((x.SessionType == ChatSessionType.AssignQuest &&
                                              x.AssociatedQuest.State == QuestState.PendingActive) ||
                                             (x.SessionType == ChatSessionType.CompleteQuest &&
                                              x.AssociatedQuest.State == QuestState.PendingComplete)))).ToList();
        }
    }
}