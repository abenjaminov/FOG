using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScriptableObjects.GameConfiguration
{
    [CreateAssetMenu(fileName = "Level Config", menuName = "Game Configuration/Levels", order = 0)]
    public class LevelConfiguration : ScriptableObject
    {
        public const int MAXLevel = 100;
        
        public List<Level> Levels;

        public Level GetLevelByOrder(int levelOrder)
        {
            return Levels.FirstOrDefault(x => x.Order == levelOrder);
        }
    }

    [Serializable]
    public class Level
    {
        public int Order;
        public int ExpForNextLevel;
        public int Points;
    }
}