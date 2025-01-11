using UnityEngine;

namespace Source.Scripts
{
    public interface IDraggable
    {
        public Transform Origin { get; }
        public Rigidbody2D Rb { get; }
        public Collider2D Collider { get; }
        public void Drag(Vector2 position);
    }
}