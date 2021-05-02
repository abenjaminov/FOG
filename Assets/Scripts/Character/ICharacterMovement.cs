using UnityEngine;

namespace Character
{
    public interface ICharacterMovement
    {
        void SetHorizontalVelocity(float horizontalVelocity);

        void SetVelocity(Vector2 velocity);

        void SetYRotation(float yRotation);
    }
}