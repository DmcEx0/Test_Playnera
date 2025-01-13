using UnityEngine;

namespace Source.Scripts.View
{
    //вспомогательный класс для дочернего коллайдера предмета, реагирующий на столкновения с другими коллайдерами
    [RequireComponent(typeof(Collider2D))]
    public class ChildTriggerView : MonoBehaviour
    {
        public bool IsInsidePlaceable { get; private set; }
        public Collider2D Collider { get; private set; }

        private void Awake()
        {
            Collider = GetComponent<Collider2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlaceableItemView placeable))
            {
                IsInsidePlaceable = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlaceableItemView placeable))
            {
                IsInsidePlaceable = false;
            }
        }
    }
}