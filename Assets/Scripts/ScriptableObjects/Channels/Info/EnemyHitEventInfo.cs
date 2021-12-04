using Entity.Enemies;
using ScriptableObjects.Inventory.ItemMetas;

namespace ScriptableObjects.Channels.Info
{
    public class EnemyHitEventInfo
    {
        public WeaponItemMeta Weapon;
        public float Damage;
        public Enemy Enemy;
    }
}