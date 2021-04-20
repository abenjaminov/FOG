using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Channel", menuName = "Channels/Player Channel", order = 0)]
    public class PlayerChannel : ScriptableObject
    {
        [Description("bool isOnGround")] 
        public UnityAction<bool> GroundCheckEvent;

        [Description("Vector2 velocity")]
        public UnityAction<Vector2> VelocityChangedEvent;

        public void OnGroundCheckEvent(bool isOnGround)
        {
            GroundCheckEvent?.Invoke(isOnGround);
        }

        public void OnVeloctyChanged(Vector2 velocity)
        {
            VelocityChangedEvent?.Invoke(velocity);
        }
    }
}