using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Source.Scripts.Models
{
    public interface IPlayerInputModel
    {
        public IReadOnlyAsyncReactiveProperty<Vector3> PointerWorldPosition { get; }
        public bool IsInteractionWithItem { get; }
        public Vector3 StartPointerPosition { get;}
        
        public void SetPointerWorldPosition(Vector3 pointerWorldPosition);
        public void SetIsInteractionWithItemState(bool isInteractionWithItem);
        public void SetStartPointerPosition(Vector3 startPointerPosition);
    }
}