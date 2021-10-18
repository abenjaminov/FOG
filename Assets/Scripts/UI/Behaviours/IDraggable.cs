using UnityEngine;

namespace UI.Behaviours
{
    public interface IDraggable
    {
        Sprite GetDragImage();
        GameObject GetGameObject();
    }
}