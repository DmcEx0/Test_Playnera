using UnityEngine;

namespace Source.Scripts
{
    public class DraggableItem : MonoBehaviour, IDraggable
    {
        [field: SerializeField] public Transform Origin { get; private set; }
        [field: SerializeField] public Collider2D MainCollider { get; private set; }
        
        public void Drag(Vector2 position)
        {
            
        }
    }
}