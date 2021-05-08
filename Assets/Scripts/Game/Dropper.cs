using System;
using System.Collections.Generic;
using ScriptableObjects.Inventory;
using UnityEngine;

namespace Game
{
    public class Dropper : MonoBehaviour
    {
        [SerializeField] private List<DropItem> _dropItems;
        [SerializeField] private Transform _dropPosition;
        [SerializeField] private Drop _dropPrefab;

        public void Drop()
        {
            InstantiateDrop(_dropItems[0]);
        }

        void InstantiateDrop(DropItem item)
        {
            var drop = Instantiate(_dropPrefab, _dropPosition.position, Quaternion.identity).GetComponent<Drop>();
            drop.SetSprite(item.ItemData.ItemSprite);
        }
    }

    [Serializable]
    public class DropItem
    {
        public InventoryItem ItemData;
        public float Percentage;
    }
}