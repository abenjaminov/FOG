using System;
using Assets.HeroEditor.Common.CommonScripts;
using Entity.NPCs;
using Helpers;
using ScriptableObjects.Channels;
using ScriptableObjects.Chat;
using ScriptableObjects.GameConfiguration;
using ScriptableObjects.Inventory;
using TMPro;
using UI.Mouse;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class ChatScreen : MonoBehaviour, ISingleClickHandler
    {
        [SerializeField] private TextPhraseMapper _textPhraseMapper;
        [SerializeField] private NpcChannel _npcChannel;
        [SerializeField] private InputChannel _inputChannel;
        [SerializeField] private float _timeBetweenCharacters;

        [SerializeField] private TextMeshProUGUI _textArea;
        [SerializeField] private Button _buttonRight;
        [SerializeField] private Button _buttonLeft;
        [SerializeField] private Inventory _playerInventory;
        private TextMeshProUGUI _buttonRightText;
        private TextMeshProUGUI _buttonLeftText;

        private KeySubscription _escapeSubscription;
        
        private float _currentTimeBetweenCharacters;
        private int _currentCharacterIndex;
        
        private ChatNpc _currentChatNpc; 
        private ChatSession _currentChatSession;
        private ChatItem _currentChatItem;
        private string _currentChatItemText;
        private int _currentChatItemIndex;
        private bool _isChatWriting;

        private void Awake()
        {
            _buttonRightText = _buttonRight.GetComponentInChildren<TextMeshProUGUI>();
            _buttonLeftText = _buttonLeft.GetComponentInChildren<TextMeshProUGUI>();

            _escapeSubscription = _inputChannel.SubscribeKeyDown(KeyCode.Escape, EscapeKeyPressed);
        }

        private void OnDestroy()
        {
            _escapeSubscription.Unsubscribe();
        }

        private void EscapeKeyPressed()
        {
            
        }

        public void StartChat(ChatNpc chatNpc, ChatSession chatSession)
        {
            _currentChatNpc = chatNpc;
            _currentChatSession = chatSession;

            _textArea.text = "";
            _currentChatItemIndex = 0;
            
            SetCurrentChatItem();
            
            _isChatWriting = true;
            
            _npcChannel.OnChatStarted(_currentChatNpc, _currentChatSession);
        }

        private void SetCurrentChatItem()
        {
            _currentChatItem = _currentChatSession.ChatItems[_currentChatItemIndex];
            _currentCharacterIndex = 0;
            _currentTimeBetweenCharacters = _timeBetweenCharacters;

            _currentChatItemText = _textPhraseMapper.RephraseText(_currentChatItem.Text);

            _buttonRightText.text = _currentChatItem.Options[0].Text;

            if (_currentChatItem.Options.Count > 1)
            {
                _buttonLeft.SetActive(true);
                _buttonLeftText.text = _currentChatItem.Options[1].Text;
            }
            else
            {
                _buttonLeft.SetActive(false);
            }
        }

        private void Update()
        {
            if (!_isChatWriting) return;

            _currentTimeBetweenCharacters -= Time.deltaTime;

            if (_currentTimeBetweenCharacters <= 0)
            {
                _textArea.SetText(_currentChatItemText.Substring(0, _currentCharacterIndex));
                SetChatItemTextIndex(_currentCharacterIndex + 1);
            }
        }

        private void SetChatItemTextIndex(int index)
        {
            _currentCharacterIndex = index;
            _currentTimeBetweenCharacters = _timeBetweenCharacters;

            if (_currentCharacterIndex > _currentChatItemText.Length)
            {
                _isChatWriting = false;
            }
        }

        private void NextChatItem()
        {
            _currentChatItemIndex++;
            SetCurrentChatItem();
            _isChatWriting = true;
        }

        private void PreviousChatItem()
        {
            _currentChatItemIndex--;
            SetCurrentChatItem();
            _isChatWriting = true;
        }
        
        // Called from Unity Event
        public void OnButtonRightAction()
        {
            OnButtonAction(0);
        }
        
        // Called from Unity Event
        public void OnButtonLeftAction()
        {
            OnButtonAction(1);
        }

        private void OnButtonAction(int buttonIndex)
        {
            var option = _currentChatItem.Options[buttonIndex];
            if (option.Action == ChatDialogOptionAction.Continue)
            {
                NextChatItem();
            } 
            else if (option.Action == ChatDialogOptionAction.Close || option.Action == ChatDialogOptionAction.Accept)
            {
                CloseScreen(option.Action);
            }
            else if (option.Action == ChatDialogOptionAction.Back)
            {
                PreviousChatItem();   
            }
        }

        private void CloseScreen(ChatDialogOptionAction reason)
        {
            if (reason == ChatDialogOptionAction.Accept)
            {
                GiveItems();
            }
            
            _npcChannel.OnChatEnded(_currentChatNpc, _currentChatSession, reason);
            
            this.SetActive(false);
        }

        private void GiveItems()
        {
            foreach (var itemReward in _currentChatSession.ItemRewards)
            {
                _playerInventory.AddItem(itemReward.ItemMeta, itemReward.Amount);
            }
        }
        
        public void HandleClick()
        {
            SetChatItemTextIndex(_currentChatItemText.Length - 1);
        }
    }
}