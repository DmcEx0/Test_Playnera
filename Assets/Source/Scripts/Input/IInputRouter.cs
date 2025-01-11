using UnityEngine;

namespace Source.Scripts.Input
{
    public interface IInputRouter
    {
        public void OnEnable();
        public void OnDisable();
        public Vector2 GetPointerPosition();
    }
}