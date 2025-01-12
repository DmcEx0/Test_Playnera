using System;
using UnityEngine;

namespace Source.Scripts
{
    public interface IDraggable
    {
        public Action StartDragging { get; }
        public Action<DraggableItemView, Vector2> Dragging { get; }
        public Action EndDragging { get; }
        public void StartDrag();
        public void Drag(Vector2 position);
        public void EndDrag();
    }
}