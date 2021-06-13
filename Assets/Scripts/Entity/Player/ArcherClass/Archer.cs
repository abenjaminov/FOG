using System.Linq;
using Platformer;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;

namespace Entity.Player.ArcherClass
{
    public class Archer : Player
    {
        [HideInInspector] public Vector2 WorldMovementDirection;

        protected override void Start()
        {
            base.Start();


        }
    }
}
