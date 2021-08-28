using System;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class EquipmentDetailsPanel : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _topText;
        [SerializeField] private TextMeshProUGUI _midText;
        [SerializeField] private TextMeshProUGUI _bottomText;
        
        public void ShowItemDetails(InventoryItemMeta itemMeta)
        {
            gameObject.SetActive(true);
            _itemImage.sprite = itemMeta.InventoryItemSprite;
            transform.position = Input.mousePosition;

            if (itemMeta is PotionItemMeta potionMeta)
            {
                _topText.text = potionMeta.Name;
                _midText.text = "Gain " + potionMeta.GainAmount + " " + potionMeta.PotionType;
                _bottomText.text = "";
            }
            else if (itemMeta is WeaponItemMeta weaponMeta)
            {
                _topText.text = weaponMeta.Name;
                _midText.text = "MR : " + weaponMeta.MonsterResistance;
                _bottomText.text = "Level : " + weaponMeta.RequiredLevel;
            }
        }

        public void HideItemDetails()
        {
            gameObject.SetActive(false);
        }
    }
}