using System;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using ScriptableObjects.Traits;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class EquipmentDetailsPanel : MonoBehaviour
    {
        [SerializeField] private PlayerTraits _playerTraits;
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _topText;
        [SerializeField] private TextMeshProUGUI _midText;
        [SerializeField] private TextMeshProUGUI _bottomText;
        
        public void ShowItemDetails(InventoryItemMeta itemMeta, Vector2 position)
        {
            gameObject.SetActive(true);
            
            _itemImage.sprite = itemMeta.InventoryItemSprite;
            transform.position = position;

            if (itemMeta is PotionItemMeta potionMeta)
            {
                _topText.text = potionMeta.Name;
                _midText.text = "Gain " + potionMeta.GainAmount + " " + potionMeta.PotionType;
                _bottomText.text = "";
            }
            else if (itemMeta is WeaponItemMeta weaponMeta)
            {
                _topText.text = weaponMeta.Name;
                _midText.text = "MR : " + weaponMeta.Traits.MonsterResistance;
                
                _bottomText.text = "Level : " + weaponMeta.RequiredLevel;

                if (_playerTraits.Level < weaponMeta.RequiredLevel)
                {
                    _bottomText.color = Color.red;
                }
                else
                {
                    _bottomText.color = Color.white;
                }
            }
            else if (itemMeta is EquipmentItemMeta equipmentMeta)
            {
                _topText.text = equipmentMeta.Name;
                _midText.text = "Level : " + equipmentMeta.RequiredLevel;
                _bottomText.text = "MR : " + equipmentMeta.Traits.MonsterResistance + ", Defense : " + equipmentMeta.Traits.Defense;

                if (_playerTraits.Level < equipmentMeta.RequiredLevel)
                {
                    _midText.color = Color.red;
                }
                else
                {
                    _midText.color = Color.white;
                }
            }
        }

        public void HideItemDetails()
        {
            gameObject.SetActive(false);
        }
    }
}