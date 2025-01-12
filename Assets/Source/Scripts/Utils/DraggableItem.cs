using UnityEngine;

namespace Source.Scripts.Utils
{
    public class DraggableItem
    {
        private DraggableItemView _draggableItemView;

        public void ConfigureWithInteraction(DraggableItemView draggableItemView , bool isInteractionWithItem)
        {
            if (isInteractionWithItem)
            {
                draggableItemView.Rb.bodyType = RigidbodyType2D.Kinematic;
                
                return;
            }
            
            draggableItemView.Rb.bodyType = RigidbodyType2D.Dynamic;
        }

        public void Drag(Vector3 position)
        {
            _draggableItemView.Drag(position);
        }
        
        public void OnStartDrag(IDraggable draggable)
        {
            
        }
        
        public bool TrySetDraggableItem(IDraggable draggable)
        {
            if(draggable is DraggableItemView draggableItem)
            {
                _draggableItemView = draggableItem;
                return true;
            }

            return false;
        }
    }
}