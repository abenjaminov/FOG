using System;
using System.Collections.Generic;
using System.Linq;
using HeroEditor.Common;
using UnityEngine;

namespace Platformer.ScriptableObjects
{
    public class InventoryMap : ScriptableObject
    {
        public SpriteCollection SpriteCollection;

        public Dictionary<string, SpriteGroupEntry> Bows;
        
        private void OnEnable()
        {
            Bows = SpriteCollection.Bow.ToDictionary(bow => bow.Id, bow => bow);
        }
    }
}