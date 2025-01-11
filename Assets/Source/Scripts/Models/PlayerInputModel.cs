using UnityEngine;

namespace Source.Scripts.Models
{
    public class PlayerInputModel
    {
        public Vector3 PointerWorldPosition { get; private set; }
        
        public void SetPointerWorldPosition(Vector3 pointerWorldPosition)
        {
            PointerWorldPosition = pointerWorldPosition;
        }
    }
}