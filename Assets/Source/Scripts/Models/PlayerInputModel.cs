using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Source.Scripts.Models
{
    public class PlayerInputModel
    {
        private readonly AsyncReactiveProperty<bool> _isInteractionWithItem;
        private readonly AsyncReactiveProperty<Vector3> _pointerWorldPosition;
        public IReadOnlyAsyncReactiveProperty<bool> IsInteractionWithItem => _isInteractionWithItem;
        public IReadOnlyAsyncReactiveProperty<Vector3> PointerWorldPosition => _pointerWorldPosition;
        

        public PlayerInputModel()
        {
            _isInteractionWithItem = new AsyncReactiveProperty<bool>(false);
            _pointerWorldPosition = new AsyncReactiveProperty<Vector3>(Vector3.zero);
        }
        
        public void SetPointerWorldPosition(Vector3 pointerWorldPosition)
        {
            _pointerWorldPosition.Value = pointerWorldPosition;
        }
        
        public void SetIsInteractionWithItemState(bool isInteractionWithItem)
        {
            _isInteractionWithItem.Value = isInteractionWithItem;
        }
    }
}