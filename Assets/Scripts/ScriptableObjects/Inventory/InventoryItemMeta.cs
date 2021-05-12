using UnityEngine;

namespace ScriptableObjects.Inventory
{
    [CreateAssetMenu(fileName = "Inventory Item Meta", menuName = "Inventory/Item", order = 1)]
    public class InventoryItemMeta : ScriptableObject
    {
        public string Name;
        public Sprite ItemSprite;
        public bool IsCurrency;
    }
}