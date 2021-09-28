using UnityEngine;

namespace ScriptableObjects.GameConfiguration
{
    [CreateAssetMenu(fileName = "Keyboard Configuration", menuName = "Game Configuration/Keyboard")]
    public class KeyboardConfiguration : ScriptableObject
    {
        public KeyCode WalkRight = KeyCode.RightArrow;
        public KeyCode WalkLeft = KeyCode.LeftArrow;
        public KeyCode Climb = KeyCode.UpArrow;
        public KeyCode Attack = KeyCode.LeftControl;
        public KeyCode Pickup = KeyCode.Z;
        public KeyCode OpenTraitsScreen = KeyCode.T;
        public KeyCode OpenInventoryScreen = KeyCode.I;
        public KeyCode OpenMapScreen = KeyCode.M;
    }
}