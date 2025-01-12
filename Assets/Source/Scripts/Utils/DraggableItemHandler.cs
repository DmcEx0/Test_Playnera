using DG.Tweening;
using Source.Scripts.View;
using UnityEngine;

namespace Source.Scripts.Utils
{
    public class DraggableItemHandler
    {
        private const float DragSpeed = 0.1f;

        public void Drag(DraggableItemView draggableItemView, Vector3 position)
        {
            var draggableTransform = draggableItemView.transform;
            position.z = 0;

            draggableTransform.localPosition = Vector3.Lerp(draggableTransform.position, position, DragSpeed);
        }

        public void StartDrag(DraggableItemView draggableItemView)
        {
            draggableItemView.transform.DOScale(1.2f, 0.3f);
            draggableItemView.Rb.bodyType = RigidbodyType2D.Kinematic;
            draggableItemView.Collider.isTrigger = true;
        }

        public void EndDrag(DraggableItemView draggableItemView)
        {
            draggableItemView.transform.DOScale(1f, 0.5f);

            if (draggableItemView.IsInsidePlaceable == false)
            {
                draggableItemView.Rb.bodyType = RigidbodyType2D.Dynamic;
                draggableItemView.Collider.isTrigger = false;
            }
        }
    }
}