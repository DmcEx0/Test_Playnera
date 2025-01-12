using Source.Scripts.View;
using UnityEngine;

namespace Source.Scripts.Utils
{
    [RequireComponent(typeof(Collider2D))]
    public class ChildTriggerHandler : MonoBehaviour
    {
        public bool IsInsidePlaceable { get; private set; }
        public Collider2D Collider { get; private set; }

        private void Awake()
        {
            Collider = GetComponent<Collider2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out IPlaceable placeable))
            {
                IsInsidePlaceable = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out IPlaceable placeable))
            {
                IsInsidePlaceable = false;
            }
        }
    }
}