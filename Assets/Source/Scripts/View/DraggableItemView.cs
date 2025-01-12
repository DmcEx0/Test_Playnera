using System;
using Source.Scripts.Utils;
using UnityEngine;

namespace Source.Scripts.View
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class DraggableItemView : MonoBehaviour, IDraggable
    {
        [field: SerializeField] public Transform Origin { get; private set; }
        [field: SerializeField] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField] public Collider2D Collider { get; private set; }
        [field: SerializeField] public ChildTriggerHandler ChildTriggerHandler { get; private set; }
    }
}