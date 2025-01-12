using DG.Tweening;
using Source.Scripts.Configs;
using Source.Scripts.View;
using UnityEngine;

namespace Source.Scripts.Utils
{
    //класс-обработчик для предметов
    public class DraggableItemHandler
    {
        private const float DefaultScale = 1f;

        private readonly GameConfig _gameConfig;
        private readonly SpriteRenderer _background;

        private Vector3 _dragOffset;

        public DraggableItemHandler(GameConfig gameConfig, GameObject background)
        {
            _gameConfig = gameConfig;
            _background = background.GetComponent<SpriteRenderer>();
        }

        public void Drag(DraggableItemView draggableItemView, Vector3 pointerCurrentPosition)
        {
            var draggableTransform = draggableItemView.transform;

            var targetPosition = pointerCurrentPosition + _dragOffset;
            targetPosition.z = 0;

            draggableTransform.localPosition = Vector3.MoveTowards(draggableTransform.position, targetPosition,
                _gameConfig.DragSpeed * Time.deltaTime);
        }

        public void StartDrag(DraggableItemView draggableItemView, Vector3 pointerStartPosition)
        {
            _dragOffset = draggableItemView.transform.position - pointerStartPosition;

            draggableItemView.transform.DOScale(_gameConfig.ItemScaleOnDrag, _gameConfig.ItemAnimationDurationOnDrag);
            draggableItemView.Rb.bodyType = RigidbodyType2D.Kinematic;
            draggableItemView.ChildTriggerHandler.Collider.isTrigger = true;
        }

        public void EndDrag(DraggableItemView draggableItemView)
        {
            var endScaleValue = GetScaleYRelativeToBackground(draggableItemView);
            draggableItemView.transform.DOScale(endScaleValue, _gameConfig.ItemAnimationDurationAfterDrag);

            if (draggableItemView.ChildTriggerHandler.IsInsidePlaceable == false)
            {
                draggableItemView.Rb.bodyType = RigidbodyType2D.Dynamic;
                draggableItemView.ChildTriggerHandler.Collider.isTrigger = false;
            }
        }

        //вычисляем размер предмета относительно высоты фона. Эффект перспективы
        private Vector3 GetScaleYRelativeToBackground(DraggableItemView draggableItemView)
        {
            var bgHeight = _background.bounds.size.y;
            float currentY = draggableItemView.transform.position.y - GetDistanceToCollidersUnderItem(draggableItemView);
            
            float minY = (bgHeight / 2) - bgHeight;
            float maxY = (bgHeight / 2);
            
            float t = Mathf.InverseLerp(minY, maxY, currentY);
            float scale = Mathf.Lerp(DefaultScale, _gameConfig.ItemMinScale, t);

            return new Vector3(scale, scale, 1);
        }

        //находим расстояние до ближайшего коллайдера под предметом для корректного определения размера 
        private float GetDistanceToCollidersUnderItem(DraggableItemView draggableItemView)
        {
            RaycastHit2D hit = Physics2D.Raycast(draggableItemView.ChildTriggerHandler.transform.position,
                Vector2.down, 100f, ~_gameConfig.ItemLayerMask);
            
            if(hit.collider == null)
            {
                return 0f;
            }
            
            Debug.Log(hit.collider.gameObject.name);
            
            return hit.distance;
        }
    }
}