using System.Collections.Generic;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
using ScriptableObjects.Traits;
using TMPro;
using UnityEngine;

namespace UI.Screens
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private InventoryChannel _inventoryChannel;
        [SerializeField] private PlayerTraits _playerTraits;
        [SerializeField] private PlayerChannel _playerChannel;
        [SerializeField] TextMeshProUGUI _infoTextPrefab;
        [SerializeField] private int _maxInfos;
        [SerializeField] private float _fadingTime;
        private int _nextInfoIndex = 0;
        private RectTransform _rectTransform;
        private float _initialYForText;
        private float _textHeight;

        private List<FadingInfoText> _allTexts = new List<FadingInfoText>();
        
        void Awake()
        {
            _playerChannel.GainedResistancePointsEvent += GainedResistancePointsEvent;
            _inventoryChannel.ItemAmountChangedEvent += ItemAmountChanged;
            _inventoryChannel.FailedToUseItemEvent += FailedToUseItemEvent;
            
            _rectTransform = GetComponent<RectTransform>();
            _textHeight = _infoTextPrefab.rectTransform.sizeDelta.y;
            _initialYForText = _textHeight / 2;

            for (int i = 0; i < _maxInfos; i++)
            {
                var infoText = Instantiate(_infoTextPrefab, Vector2.zero, Quaternion.identity, this.transform);
                infoText.gameObject.SetActive(false);
                _allTexts.Add(new FadingInfoText()
                {
                    FadingTime = _fadingTime,
                    TextMesh = infoText
                });
            }
        }

        private void FailedToUseItemEvent(InventoryItem item)
        {
            AddInfoItem("Cannot use this item right now");
        }

        private void Update()
        {
            for (int i = 0; i < _allTexts.Count; i++)
            {
                if (!_allTexts[i].TextMesh.gameObject.activeInHierarchy) continue;
                
                _allTexts[i].FadingTime -= Time.deltaTime;
                _allTexts[i].TextMesh.alpha = _allTexts[i].FadingTime / _fadingTime;
                
                if (_allTexts[i].FadingTime <= 0)
                {
                    _allTexts[i].TextMesh.gameObject.SetActive(false);
                    i--;
                    _nextInfoIndex = (_nextInfoIndex + _maxInfos - 1) % _maxInfos;
                }
            }
        }

        private void ItemAmountChanged(InventoryItem item, int amountAdded)
        {
            var message = item.ItemMeta.GetAmountChangedText(amountAdded);

            if (message == string.Empty) return;
            
            AddInfoItem(message);
        }

        private void GainedResistancePointsEvent(float gained)
        {
            if (gained == 0) return;
            
            AddInfoItem(gained + " RP Gained");
        }
        
        private void AddInfoItem(string itemText)
        {
            var infoText = _allTexts[_nextInfoIndex].TextMesh;
            infoText.rectTransform.localPosition =
                new Vector2(-_rectTransform.sizeDelta.x, _initialYForText - _textHeight);
            infoText.SetText(itemText);

            foreach (var text in _allTexts)
            {
                text.TextMesh.rectTransform.localPosition += new Vector3(0, _textHeight, 0);
            }

            _allTexts[_nextInfoIndex].FadingTime = _fadingTime;
            infoText.gameObject.SetActive(true);
            _nextInfoIndex = (_nextInfoIndex + 1) % _maxInfos;
        }

        private void OnDestroy()
        {
            _playerChannel.GainedResistancePointsEvent -= GainedResistancePointsEvent;
            _inventoryChannel.ItemAmountChangedEvent -= ItemAmountChanged;
            _inventoryChannel.FailedToUseItemEvent -= FailedToUseItemEvent;
        }
    }

    public class FadingInfoText
    {
        public float FadingTime;
        public TextMeshProUGUI TextMesh;
    }
}
