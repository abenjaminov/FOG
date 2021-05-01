using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level Config", menuName = "Game Stats/Level Config", order = 0)]
    public class LevelConfiguration : ScriptableObject
    {
        public const int MAX_LEVEL = 100;
        
        public List<Level> Levels;
    }

    [Serializable]
    public class Level
    {
        public int Order;
        public int fromExp;
    }
}