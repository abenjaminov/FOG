using Helpers;
using ScriptableObjects.Inventory.ItemMetas;
using ScriptableObjects.Traits;
using UnityEngine;

namespace Game
{
    public class Dropper : MonoBehaviour
    {
        [SerializeField] private Transform _dropPosition;
        [SerializeField] private Drop _dropPrefab;
        
        
        public void InstantiateDrop(InventoryItemMeta itemMeta, int amount,float xPositionOffset = 0)
        {
            var position = _dropPosition.position + new Vector3(xPositionOffset, 0, 0);
            var drop = Instantiate(_dropPrefab, position, Quaternion.identity).GetComponent<Drop>();
            drop.SetInventoryItemMeta(itemMeta, amount);
        }
    }
}