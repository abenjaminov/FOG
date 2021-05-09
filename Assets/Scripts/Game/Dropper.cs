using System;
using System.Collections.Generic;
using ScriptableObjects.Inventory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class Dropper : MonoBehaviour
    {
        [SerializeField] private List<DropItem> _dropItems;
        [SerializeField] private Transform _dropPosition;
        [SerializeField] private Drop _dropPrefab;
        [SerializeField] private float multiDropOffset;

        public void Drop()
        {
            int numberOfInstantiated = 0;
            
            foreach (var dropItem in _dropItems)
            {
                var randomNumber = Random.Range(0, 1);
                if (randomNumber <= dropItem.Percentage)
                {
                    var offsetValue = (numberOfInstantiated + 1) / 2;
                    float offset = numberOfInstantiated % 2 == 0 ? offsetValue : -offsetValue; 
                    InstantiateDrop(dropItem, offset * multiDropOffset);
                    
                    numberOfInstantiated++;
                }
            }
        }

        void InstantiateDrop(DropItem item, float xPositionOffset)
        {
            var position = _dropPosition.position + new Vector3(xPositionOffset, 0, 0);
            var drop = Instantiate(_dropPrefab, position, Quaternion.identity).GetComponent<Drop>();
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