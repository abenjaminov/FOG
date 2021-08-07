using System;
using Assets.HeroEditor.Common.CommonScripts;
using ScriptableObjects.Channels;
using ScriptableObjects.Chat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class ChatScreen : MonoBehaviour
    {
        [SerializeField] private float _timeBetweenCharacters;

        [SerializeField] private TextMeshProUGUI _textArea;
        [SerializeField] private Button _buttonRight;
        [SerializeField] private Button _buttonLeft;
        [SerializeField] private QuestsChannel _questsChannel;
        private Text _buttonRightText;
        private Text _buttonLeftText;
        
        private float _currentTimeBetweenCharacters;
        private int _currentCharacterIndex;
        
        public ChatSession CurrentChatSession;
        private ChatItem _currentChatItem;
        private int _currentChatItemIndex;
        private bool _isChatWriting;

        private void Awake()
        {
            _buttonRightText = _buttonRight.GetComponentInChildren<Text>();
            _buttonLeftText = _buttonLeft.GetComponentInChildren<Text>();
        }

        public void StartChat()
        {
            _textArea.text = "";
            _currentChatItemIndex = 0;
            
            SetCurrentChatItem();
            
            _isChatWriting = true;
        }

        private void SetCurrentChatItem()
        {
            _currentChatItem = CurrentChatSession.ChatItems[_currentChatItemIndex];
            _currentCharacterIndex = 0;
            _currentTimeBetweenCharacters = _timeBetweenCharacters;
            
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
                _textArea.SetText(_currentChatItem.Text.Substring(0, _currentCharacterIndex));
                _currentCharacterIndex++;
                _currentTimeBetweenCharacters = _timeBetweenCharacters;

                if (_currentCharacterIndex > _currentChatItem.Text.Length)
                {
                    _isChatWriting = false;
                }
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
            else if (option.Action == ChatDialogOptionAction.Close)
            {
                CloseScreen();
            }
            else if (option.Action == ChatDialogOptionAction.Back)
            {
                PreviousChatItem();   
            }
            else if (option.Action == ChatDialogOptionAction.AssignQuest)
            {
                _questsChannel.AssignQuest(CurrentChatSession.AssociatedQuest);
                CloseScreen();
            }
            else if (option.Action == ChatDialogOptionAction.CompleteQuest)
            {
                CurrentChatSession.AssociatedQuest.GiveRewards();
                CloseScreen();
            }
        }

        private void CloseScreen()
        {
            this.SetActive(false);
        }
    }
}