using System;
using System.Collections.Generic;
using Assets.HeroEditor.Common.CommonScripts;
using ScriptableObjects.Channels;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
        private TextMeshProUGUI _buttonRightText;
        private TextMeshProUGUI _buttonLeftText;

        private List<MessageOptions> _currentMessageOptions;
        UnityAction<MessageOptions> _currentCallback;
        
        private void Awake()
        {
            _buttonRightText = _buttonRight.GetComponentInChildren<TextMeshProUGUI>();
            _buttonLeftText = _buttonLeft.GetComponentInChildren<TextMeshProUGUI>();
            
            _gameChannel.ShowMessageRequest += ShowMessageRequest;
        }

        private void OnDestroy()
        {
            _gameChannel.ShowMessageRequest -= ShowMessageRequest;
        }

        private void ShowMessageRequest(string message, List<MessageOptions> options, UnityAction<MessageOptions> callback)
        {
            if (options.Count == 0) return;
            
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
            _currentCallback = callback;
        }

        public void LeftButtonClicked()
        {
            _screen.SetActive(false);
            _currentCallback?.Invoke(_currentMessageOptions[0]);
        }
        
        public void RightButtonClicked()
        {
            _screen.SetActive(false);
            _currentCallback?.Invoke(_currentMessageOptions[1]);
        }
    }
}