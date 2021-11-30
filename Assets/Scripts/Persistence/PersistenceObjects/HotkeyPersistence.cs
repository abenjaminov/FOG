using System;
using UnityEngine;

namespace Persistence.PersistenceObjects
{
    [Serializable]
    public class HotkeyPersistence
    {
        public KeyCode Key;
        public string Name;
        public string ItemId;
    }
}