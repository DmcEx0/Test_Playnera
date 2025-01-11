using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Source.Scripts.Models
{
    public class PlayerInputModel
    {
        private readonly AsyncReactiveProperty<bool> _isPressed;
        private readonly AsyncReactiveProperty<bool> _isInteractionWithItem;
        public IReadOnlyAsyncReactiveProperty<bool> IsPressed => _isPressed;
        public IReadOnlyAsyncReactiveProperty<bool> IsInteractionWithItem => _isInteractionWithItem;
        
        public Vector3 PointerWorldPosition { get; private set; }

        public PlayerInputModel()
        {
            _isPressed = new AsyncReactiveProperty<bool>(false);
            _isInteractionWithItem = new AsyncReactiveProperty<bool>(false);
        }
        
        public void SetPointerWorldPosition(Vector3 pointerWorldPosition)
        {
            PointerWorldPosition = pointerWorldPosition;
        }
        
        public void SetPressedValue(bool isPressed)
        {
            _isPressed.Value = isPressed;
        }
        
        public void SetIsInteractionWithItemState(bool isInteractionWithItem)
        {
            _isInteractionWithItem.Value = isInteractionWithItem;
        }
    }
}