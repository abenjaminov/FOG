using UnityEngine;

namespace Cheats
{
    public class DebugCheats : MonoBehaviour
    {
        [SerializeField] private Entity.Player.Player _player;
        
        [ContextMenu("Fill Player Health")]
        public void FillPlayerHealth()
        {
            _player.ChangeHealth(_player.Traits.MaxHealth);
        }
    }
}