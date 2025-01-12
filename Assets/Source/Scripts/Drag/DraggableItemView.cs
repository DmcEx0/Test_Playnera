using System;
using UnityEngine;

namespace Source.Scripts
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class DraggableItemView : MonoBehaviour, IDraggable
    {
        private const float DragSpeed = 0.1f;
        
        [field: SerializeField] public Transform Origin { get; private set; }
        [field: SerializeField] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField] public Collider2D Collider { get; private set; }

        public Action StartDragging { get; private set;}
        public Action<DraggableItemView, Vector2> Dragging { get; private set;}
        public Action EndDragging { get; private set;}

        public void StartDrag()
        {
            StartDragging?.Invoke();
        }

        public void Drag(Vector2 position)
        {
            Dragging?.Invoke(this, position);
            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.position, position, DragSpeed);
        }

        public void EndDrag()
        {
            EndDragging?.Invoke();
        }
    }
}