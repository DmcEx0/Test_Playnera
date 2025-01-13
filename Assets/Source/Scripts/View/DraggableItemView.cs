using UnityEngine;

namespace Source.Scripts.View
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class DraggableItemView : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField] public ChildTriggerView ChildTriggerView { get; private set; }
    }
}