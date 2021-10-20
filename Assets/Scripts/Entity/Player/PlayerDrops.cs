using System;
using Game;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
using UI.Behaviours;
using UI.Screens;
using UnityEngine;

namespace Entity.Player
{
    public class PlayerDrops : MonoBehaviour
    {
        [SerializeField] private InventoryChannel _inventoryChannel;
        [SerializeField] private DragChannel _dragChannel;
        [SerializeField] private Inventory _playerInventory;
        [SerializeField] Dropper _dropper;

        private void Awake()
        {
            _inventoryChannel.DropItemRequestEvent += DropItemRequestEvent;
            _dragChannel.EndDragVoidEvent += EndDragVoidEvent;
        }

        private void EndDragVoidEvent(IDraggable draggable)
        {
            if (!draggable.GetGameObject().TryGetComponent(typeof(InventoryItemView), out var component)) return;
            var itemToDrop = ((InventoryItemView)component).InventoryItem;

            DropItemRequestEvent(itemToDrop);
        }

        private void DropItemRequestEvent(InventoryItem item)
        {
            _dropper.InstantiateDrop(item.ItemMeta,item.Amount);
            _playerInventory.RemoveItem(item);
        }

        private void OnDestroy()
        {
            _inventoryChannel.DropItemRequestEvent -= DropItemRequestEvent;
            _dragChannel.EndDragVoidEvent -= EndDragVoidEvent;
        }
    }
}