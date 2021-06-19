using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Entity.Player.ArcherClass
{
    public class Archer : Player
    {
        [HideInInspector] public Vector2 WorldMovementDirection;
    }
}
