using UnityEngine;

namespace State
{
    public interface IAbilityState : IState
    {
        KeyCode GetHotKey();
        void SetHotKeyDown(bool isDown);
        bool IsHotKeyDown();
    }
}