namespace Platformer
{
    public interface IHealthUI
    {
        void SetHealth(float percentage);
        void ToggleUI(bool isActive);
    }
}