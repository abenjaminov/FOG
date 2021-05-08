using UnityEngine;

namespace ScriptableObjects.Inventory
{
    [CreateAssetMenu(fileName = "Inventory Item", menuName = "Inventory/Item", order = 1)]
    public class InventoryItem : ScriptableObject
    {
        public string Name;
        public Sprite ItemSprite;
    }
}