using System;
using System.Collections.Generic;

namespace Persistence.PersistenceObjects
{
    [Serializable]
    public class PlayerInventoryPersistence
    {
        public List<PlayerInventoryItemPersistence> OwnedItems = new List<PlayerInventoryItemPersistence>();
        public PlayerInventoryItemPersistence CurrencyItem;
    }
}