using Entity;
using UnityEngine;

namespace Abilities
{
    public abstract class Attack : Ability
    {
        public int NumberOfEnemies;


        protected Attack(WorldEntity host, KeyCode hotKey, int numberOfEnemies) : base(host, hotKey)
        {
            NumberOfEnemies = numberOfEnemies;
        }
    }
}