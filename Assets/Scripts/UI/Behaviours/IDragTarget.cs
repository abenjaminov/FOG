namespace UI.Behaviours
{
    public interface IDragTarget
    {
        void DragDropped(IDraggable draggable);
        bool IsEmpty();
    }
}