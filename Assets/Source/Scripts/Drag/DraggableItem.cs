using UnityEngine;

namespace Source.Scripts
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class DraggableItem : MonoBehaviour, IDraggable
    {
        [field: SerializeField] public Transform Origin { get; private set; }
        
        public void Drag(Vector2 position)
        {
            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.position, position, 0.1f);
        }
    }
}