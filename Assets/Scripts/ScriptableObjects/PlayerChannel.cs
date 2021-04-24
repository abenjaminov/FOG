using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Channel", menuName = "Channels/Player Channel", order = 0)]
    public class PlayerChannel : ScriptableObject
    {
        public bool IsWalking;
        public bool IsJumping;
        public Vector2 FaceDirection;

        [Description("bool isOnGround")] 
        public UnityAction<bool> GroundCheckEvent;

        [Description("Vector2 velocity")]
        public UnityAction<Vector2> VelocityChangedEvent;
        
        public UnityAction Attack1Event;
        public UnityAction<bool> MovementActiveEvent;

        public void OnGroundCheckEvent(bool isOnGround)
        {
            GroundCheckEvent?.Invoke(isOnGround);
        }

        public void OnVeloctyChanged(Vector2 velocity)
        {
            VelocityChangedEvent?.Invoke(velocity);
        }

        public void OnAttack1()
        {
            Attack1Event?.Invoke();   
        }

        public void OnMovementActive(bool isActive)
        {
            MovementActiveEvent?.Invoke(isActive);
        }
    }
}