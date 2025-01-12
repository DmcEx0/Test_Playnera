using System;
using UnityEngine;

namespace Source.Scripts.View
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class DraggableItemView : MonoBehaviour, IDraggable
    {
        [field: SerializeField] public Transform Origin { get; private set; }
        [field: SerializeField] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField] public Collider2D Collider { get; private set; }
        
        public bool IsInsidePlaceable { get; private set; }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("!!!");
            if(other.collider.TryGetComponent(out IPlaceable placeable))
            {
                Debug.Log("???");

                IsInsidePlaceable = true;
            }
        }
        
        private void OnCollisionExit(Collision other)
        {
            if(other.collider.TryGetComponent(out IPlaceable placeable))
            {
                IsInsidePlaceable = false;
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("!!!");

            if(other.TryGetComponent(out IPlaceable placeable))
            {
                Debug.Log("???");

                IsInsidePlaceable = false;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent(out IPlaceable placeable))
            {
                IsInsidePlaceable = false;
            }
        }
    }
}