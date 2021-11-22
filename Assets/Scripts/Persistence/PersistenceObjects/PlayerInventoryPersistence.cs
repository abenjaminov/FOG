using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Persistence.PersistenceObjects
{
    [Serializable]
    public class PlayerInventoryPersistence
    {
        public List<PlayerInventoryItemPersistence> OwnedItems = new List<PlayerInventoryItemPersistence>();
        public PlayerInventoryItemPersistence CurrencyItem;
        public Dictionary<KeyCode, string> KeyCodeToItemIdMap = new Dictionary<KeyCode, string>();
    }
}