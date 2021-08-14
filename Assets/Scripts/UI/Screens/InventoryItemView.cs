using System;
using TMPro;
using UI.Mouse;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    class InventoryItemView : MonoBehaviour, IDoubleClickHandler
    {
        public Image ItemSprite;
        public TextMeshProUGUI AmountText;
        public Action<InventoryItemView> ItemViewDoubleClicked;
            
        public void HandleDoubleClick()
        {
            ItemViewDoubleClicked?.Invoke(this);
        }
    }
}