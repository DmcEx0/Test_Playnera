using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Source.Scripts.Models
{
    //модель для связи между контроллерами
    public class PlayerInputModel : IPlayerInputModel
    {
        private readonly AsyncReactiveProperty<Vector3> _pointerWorldPosition;
        public IReadOnlyAsyncReactiveProperty<Vector3> PointerWorldPosition => _pointerWorldPosition;
        public bool IsInteractionWithItem { get; private set; }
        public Vector3 StartPointerPosition { get; private set; }

        public PlayerInputModel()
        {
            _pointerWorldPosition = new AsyncReactiveProperty<Vector3>(Vector3.zero);
        }
        
        public void SetPointerWorldPosition(Vector3 pointerWorldPosition)
        {
            _pointerWorldPosition.Value = pointerWorldPosition;
        }
        
        public void SetIsInteractionWithItemState(bool isInteractionWithItem)
        {
            IsInteractionWithItem = isInteractionWithItem;
        }
        
        public void SetStartPointerPosition(Vector3 startPointerPosition)
        {
            StartPointerPosition = startPointerPosition;
        }
    }
}