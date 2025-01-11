namespace Source.Scripts.Models
{
    public class DraggableModel
    {
        public IDraggable Draggable { get; private set; }

        public void SetDraggable(IDraggable draggable)
        {
            Draggable = draggable;
        }
    }
}