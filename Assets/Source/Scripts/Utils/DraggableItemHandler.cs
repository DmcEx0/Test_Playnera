using DG.Tweening;
using Source.Scripts.View;
using UnityEngine;

namespace Source.Scripts.Utils
{
    public class DraggableItemHandler
    {
        private const float DragSpeed = 25f;

        private Vector3 _dragOffset;

        public void Drag(DraggableItemView draggableItemView, Vector3 pointerCurrentPosition)
        {
            var draggableTransform = draggableItemView.transform;

            var targetPosition = pointerCurrentPosition + _dragOffset;
            targetPosition.z = 0;

            draggableTransform.localPosition = Vector3.MoveTowards(draggableTransform.position, targetPosition,
                DragSpeed * Time.deltaTime);
        }

        public void StartDrag(DraggableItemView draggableItemView, Vector3 pointerStartPosition)
        {
            _dragOffset = draggableItemView.transform.position - pointerStartPosition;

            draggableItemView.transform.DOScale(1.2f, 0.3f);
            draggableItemView.Rb.bodyType = RigidbodyType2D.Kinematic;
            draggableItemView.Collider.isTrigger = true;
        }

        public void EndDrag(DraggableItemView draggableItemView)
        {
            draggableItemView.transform.DOScale(1f, 0.5f);

            if (draggableItemView.ChildTriggerHandler.IsInsidePlaceable == false)
            {
                draggableItemView.Rb.bodyType = RigidbodyType2D.Dynamic;
                draggableItemView.Collider.isTrigger = false;
            }
        }
    }
}