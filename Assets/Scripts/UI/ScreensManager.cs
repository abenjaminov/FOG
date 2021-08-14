using System;
using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor.Common.CommonScripts;
using Entity.NPCs;
using ScriptableObjects.Channels;
using ScriptableObjects.Chat;
using ScriptableObjects.Quests;
using TMPro;
using UI.Screens;
using UnityEngine;

namespace Platformer.UI
{
    public class ScreensManager : MonoBehaviour
    {
        [SerializeField] private InputChannel _inputChannel;
        [SerializeField] private NpcChannel _NpcChannel;
        [SerializeField] private GUIScreen _traitsScreen;
        [SerializeField] private GUIScreen _inventory;
        [SerializeField] private GUIScreen _map;
        
        [SerializeField] private ChatSelectionScreen _chatSelectionScreen;
        [SerializeField] private ChatScreen _chatScreen;

        private List<KeySubscription> _subscriptions = new List<KeySubscription>();
        
        private Stack<GUIScreen> _openScreens;

        private void Awake()
        {
            _openScreens = new Stack<GUIScreen>();
            _subscriptions.Add(_inputChannel.SubscribeKeyDown(_traitsScreen.GetActivationKey(), ToggleTraitsScreen));
            _subscriptions.Add(_inputChannel.SubscribeKeyDown(_inventory.GetActivationKey(), ToggleInventoryScreen));
            _subscriptions.Add(_inputChannel.SubscribeKeyDown(_map.GetActivationKey(), ToggleMapScreen));
            
            _subscriptions.Add(_inputChannel.SubscribeKeyDown(KeyCode.Escape, ClosePrevScreen));
            
            _NpcChannel.RequestChatStartEvent += RequestChatStartEvent;
        }

        private void ToggleTraitsScreen()
        {
            ToggleScreen(_traitsScreen);
        }
        
        private void ToggleInventoryScreen()
        {
            ToggleScreen(_inventory);
        }
        
        private void ToggleMapScreen()
        {
            ToggleScreen(_map);
        }
        
        private void OnDestroy()
        {
            foreach (var subscription in _subscriptions)
            {
                subscription.Unsubscribe();
            }
            
            _subscriptions.Clear();
        }

        private void RequestChatStartEvent(ChatNpc chatNpc)
        {
            if (!_chatScreen.isActiveAndEnabled)
            {
                var availableSessions = chatNpc.ChatSessions.Where(x => x.SessionType == ChatSessionType.Casual ||
                                                                        (x.SessionType == ChatSessionType.AssignQuest &&
                                                                         x.AssociatedQuest.State == QuestState.PendingActive) ||
                                                                        (x.SessionType == ChatSessionType.CompleteQuest &&
                                                                         x.AssociatedQuest.State == QuestState.PendingComplete)).ToList();

                if (availableSessions.Count <= 0) return;
                
                _chatSelectionScreen.SetActive(true);
                _chatSelectionScreen.SelectChat(chatNpc, availableSessions);
            }
        }

        private void ToggleScreen(GUIScreen screenToToggle)
        {
            if (screenToToggle.IsOpen)
            {
                var openScreensLeft = _openScreens.Where(x => x != screenToToggle).ToList();
                _openScreens.Clear();
                foreach (var screen in openScreensLeft)
                {
                    _openScreens.Push(screen);
                }
            }
            else
            {
                _openScreens.Push(screenToToggle);
            }
            
            screenToToggle.ToggleView();    
        }

        private void ClosePrevScreen()
        {
            if (_openScreens.Count == 0) return;
            
            var prevScreen = _openScreens.Pop();
            
            ToggleScreen(prevScreen);
        }
    }
}