using Helpers;
using ScriptableObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
using TMPro;
using UnityEngine;

namespace UI
{
    public class InventoryScreen : MonoBehaviour
    {
        [SerializeField] private PlayerTraits _playerTraits;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private InventoryChannel _invChannel;

        [SerializeField] private TextMeshProUGUI _coinText;
        
        // Start is called before the first frame update
        void Start()
        {
            _invChannel.ItemAddedEvent += ItemAddedEvent;
        }

        private void ItemAddedEvent(InventoryItem itemAddition, InventoryItem item)
        {
            if (item.ItemMeta.IsCurrency)
            {
                _coinText.SetText(MoneyToString(item.Amount));
            }
        }

        private string MoneyToString(int money)
        {
            var result = "";
            var currentPart = "";
            
            currentPart = (money % 1000).ToString();
            
            while (money > 0)
            {
                var zerosToAdd = 3 - currentPart.Length;
                var zeros = StringHelper.MuiltiplyString("0", zerosToAdd);
                currentPart = zeros + currentPart;
                result = currentPart + result;
                result = "," + result;

                currentPart = (money % 100).ToString();
                money /= 1000;
            }

            result = result.TrimStart(',', '0');
            
            return result;
        }
    }
}
