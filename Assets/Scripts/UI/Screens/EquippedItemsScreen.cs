using System;
using Entity.Player;
using HeroEditor.Common.Enums;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;

namespace UI.Screens
{
    public class EquippedItemsScreen : GUIScreen
    {
        [SerializeField] private PlayerChannel _playerChannel;
        [SerializeField] private EquipmentItemView _helmet;
        [SerializeField] private EquipmentItemView _armour;
        [SerializeField] private EquipmentItemView _primaryWeapon;
        [SerializeField] private EquipmentItemView _cape;
        [SerializeField] private PlayerEquipment _equipment;
        [SerializeField] private EquipmentDetailsPanel _equipmentDetailsPanel;
        private PlayerAppearance _apearance;

        protected override void Awake()
        {
            base.Awake();
            
            _helmet.ItemViewMouseEnter += ItemViewMouseEnter;
            _helmet.ItemViewMouseExit += ItemViewMouseExit;
            _armour.ItemViewMouseEnter += ItemViewMouseEnter;
            _armour.ItemViewMouseExit += ItemViewMouseExit;
            _primaryWeapon.ItemViewMouseEnter += ItemViewMouseEnter;
            _primaryWeapon.ItemViewMouseExit += ItemViewMouseExit;
            _cape.ItemViewMouseEnter += ItemViewMouseEnter;
            _cape.ItemViewMouseExit += ItemViewMouseExit;
            
            _helmet.ItemViewDoubleClicked += HelmetItemViewDoubleClicked;
            _armour.ItemViewDoubleClicked += ArmourItemViewDoubleClicked;
            _cape.ItemViewDoubleClicked += CapeItemViewDoubleClicked;
            _primaryWeapon.ItemViewDoubleClicked += PrimaryWeaponItemViewDoubleClick;

            _playerChannel.ItemEquippedEvent += EquipmentChangedEvent;
            _playerChannel.ItemUnEquippedEvent += EquipmentChangedEvent;
            
            _apearance = FindObjectOfType<PlayerAppearance>();
        }

        private void OnDestroy()
        {
            _helmet.ItemViewMouseEnter -= ItemViewMouseEnter;
            _helmet.ItemViewMouseExit -= ItemViewMouseExit;
            _armour.ItemViewMouseEnter -= ItemViewMouseEnter;
            _armour.ItemViewMouseExit -= ItemViewMouseExit;
            _primaryWeapon.ItemViewMouseEnter -= ItemViewMouseEnter;
            _primaryWeapon.ItemViewMouseExit -= ItemViewMouseExit;
            _cape.ItemViewMouseEnter -= ItemViewMouseEnter;
            _cape.ItemViewMouseExit -= ItemViewMouseExit;
            
            _helmet.ItemViewDoubleClicked -= HelmetItemViewDoubleClicked;
            _armour.ItemViewDoubleClicked -= ArmourItemViewDoubleClicked;
            _cape.ItemViewDoubleClicked -= CapeItemViewDoubleClicked;
            _primaryWeapon.ItemViewDoubleClicked -= PrimaryWeaponItemViewDoubleClick;

            _playerChannel.ItemEquippedEvent -= EquipmentChangedEvent;
            _playerChannel.ItemUnEquippedEvent -= EquipmentChangedEvent;
        }

        private void EquipmentChangedEvent(EquipmentItemMeta arg0, EquipmentPart arg1)
        {
            UpdateUI();
        }

        private void PrimaryWeaponItemViewDoubleClick(EquipmentItemView primaryWeapon)
        {
            if (_equipment.PrimaryWeapon == null) return;
            
            _apearance.UnEquipItem(EquipmentPart.Bow);
            _apearance.UnEquipItem(EquipmentPart.MeleeWeapon1H);
            HideItem(_primaryWeapon);
        }

        private void CapeItemViewDoubleClicked(EquipmentItemView obj)
        {
            if (_equipment.Cape == null) return;
            
            _apearance.UnEquipItem(EquipmentPart.Cape);
            HideItem(_cape);
        }

        private void ArmourItemViewDoubleClicked(EquipmentItemView obj)
        {
            if (_equipment.Armour == null) return;
            
            _apearance.UnEquipItem(EquipmentPart.Armor);
            HideItem(_armour);
        }

        private void HelmetItemViewDoubleClicked(EquipmentItemView obj)
        {
            if (_equipment.Helmet == null) return;
            
            _apearance.UnEquipItem(EquipmentPart.Helmet);
            HideItem(_helmet);
        }

        private void ItemViewMouseExit(EquipmentItemView itemView)
        {
            _equipmentDetailsPanel.HideItemDetails();
        }

        private void ItemViewMouseEnter(EquipmentItemView itemView)
        {
            _equipmentDetailsPanel.ShowItemDetails(itemView.ItemMeta, itemView.GetBottomLeftCorner());
        }

        public override KeyCode GetActivationKey()
        {
            return _keyboardConfiguration.OpenEquippedScreen;
        }

        protected override void UpdateUI()
        {
            ShowItem(_helmet, _equipment.Helmet);
            ShowItem(_armour, _equipment.Armour);
            ShowItem(_primaryWeapon, _equipment.PrimaryWeapon);
            ShowItem(_cape, _equipment.Cape);
        }

        private void ShowItem(EquipmentItemView itemView, InventoryItemMeta itemMeta)
        {
            if (itemMeta == null) return;
            
            var color = itemView.ItemSprite.color;
            itemView.ItemSprite.sprite = itemMeta.InventoryItemSprite;
            itemView.ItemSprite.color = new Color(color.r, color.g, color.b, 255);
            itemView.ItemMeta = itemMeta;
        }

        private void HideItem(EquipmentItemView itemView)
        {
            var color = itemView.ItemSprite.color;
            itemView.ItemSprite.sprite = null;
            itemView.ItemSprite.color = new Color(color.r, color.g, color.b, 0);
            itemView.ItemMeta = null;
        }
    }
}