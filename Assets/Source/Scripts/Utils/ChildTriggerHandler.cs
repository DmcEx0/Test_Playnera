using Source.Scripts.View;
using UnityEngine;

namespace Source.Scripts.Utils
{
    public class ChildTriggerHandler : MonoBehaviour
    {
        public bool IsInsidePlaceable { get; private set; }

        private void OnTriggerStay2D(Collider2D other)
        {
            if(other.TryGetComponent(out IPlaceable placeable))
            {
                IsInsidePlaceable = true;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.TryGetComponent(out IPlaceable placeable))
            {
                IsInsidePlaceable = false;
            }
        }
    }
}