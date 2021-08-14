using System;
using ScriptableObjects.Chat;
using TMPro;
using UI.Mouse;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Screens
{
    public class ChatSelectionItem : MonoBehaviour, IClickHandler
    {
        [SerializeField] private TextMeshProUGUI _text;
        public ChatSession Session { get; set; }
        public Action<ChatSelectionItem> ChatSelectedEvent;
        private RectTransform _rectTransform;

        private BoxCollider2D _boxCollider2D;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        public void HandleClick()
        {
            ChatSelectedEvent?.Invoke(this);
        }

        public void SetText(string text)
        {
            _text.text = text;
            var textSize = _text.GetPreferredValues(text);
            this._rectTransform.sizeDelta = textSize;
            _boxCollider2D.offset = (textSize / 2) * new Vector2(1,-1);
            _boxCollider2D.size = textSize;
        }

        private void OnMouseEnter()
        {
            _text.fontStyle = FontStyles.Highlight;
        }
        
        private void OnMouseExit()
        {
            _text.fontStyle = FontStyles.Normal;
        }
    }
}