using System;

namespace Persistence.PersistenceObjects
{
    [Serializable]
    public class PlayerInventoryItemPersistence
    {
        public string Name;
        public string InventoryItemMetaId;
        public int Amount;
    }
}