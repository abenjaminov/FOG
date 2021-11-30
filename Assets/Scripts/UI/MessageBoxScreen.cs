using System;
using System.Collections.Generic;
using Assets.HeroEditor.Common.CommonScripts;
using ScriptableObjects.Channels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MessageBoxScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _screen;
        [SerializeField] private GameChannel _gameChannel;

        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private Button _buttonRight;
        [SerializeField] private Button _buttonLeft;
        [SerializeField] private TMP_InputField _inputField;
        private TextMeshProUGUI _buttonRightText;
        private TextMeshProUGUI _buttonLeftText;

        private List<MessageOptions> _currentMessageOptions;
        private string _currentMessageId;
        
        private void Awake()
        {
            _buttonRightText = _buttonRight.GetComponentInChildren<TextMeshProUGUI>();
            _buttonLeftText = _buttonLeft.GetComponentInChildren<TextMeshProUGUI>();
            
            _gameChannel.ShowMessageRequestEvent += ShowMessageRequest;
            _gameChannel.ShowInputRequestEvent += ShowInputRequest;
        }

        private void OnDestroy()
        {
            _gameChannel.ShowMessageRequestEvent -= ShowMessageRequest;
            _gameChannel.ShowInputRequestEvent -= ShowInputRequest;
        }

        private void ShowInputRequest(string messageId, string message, List<MessageOptions> options)
        {
            if (options.Count == 0)
            {
                _gameChannel.OnInputRequestClosed(messageId,"", MessageOptions.NoOptions);
                return;
            }
            
            _inputField.SetActive(true);
            _inputField.text = "";
            
            Refresh(messageId, message, options);
        }
        
        private void ShowMessageRequest(string messageId, string message, List<MessageOptions> options)
        {
            if (options.Count == 0)
            {
                _gameChannel.OnMessageClosed(messageId, MessageOptions.NoOptions);
                return;
            }
            
            _inputField.SetActive(false);
            
            Refresh(messageId, message, options);
        }

        private void Refresh(string messageId, string message, List<MessageOptions> options)
        {
            _messageText.text = message;
            _buttonLeftText.text = Enum.GetName(typeof(MessageOptions), options[0]);

            if (options.Count > 1)
            {
                _buttonRight.SetActive(true);
                _buttonRightText.text = Enum.GetName(typeof(MessageOptions), options[1]);
            }
            else
            {
                _buttonRight.SetActive(false);
            }

            _screen.SetActive(true);
            _currentMessageOptions = options;
            _currentMessageId = messageId;
        }

        public void LeftButtonClicked()
        {
            if (_inputField.IsActive())
            {
                _gameChannel.OnInputRequestClosed(_currentMessageId, _inputField.text, _currentMessageOptions[0]);
            }
            else
            {
                _gameChannel.OnMessageClosed(_currentMessageId, _currentMessageOptions[0]);
            }
            
            _screen.SetActive(false);
        }
        
        public void RightButtonClicked()
        {
            if (_inputField.IsActive())
            {
                _gameChannel.OnInputRequestClosed(_currentMessageId, _inputField.text, _currentMessageOptions[1]);
            }
            else
            {
                _gameChannel.OnMessageClosed(_currentMessageId, _currentMessageOptions[1]);
            }
            
            _screen.SetActive(false);
        }
    }
}