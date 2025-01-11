using UnityEngine;

namespace Source.Scripts.Input
{
    public class PlayerInputRouter : IInputRouter
    {
        private readonly PlayerInput _playerInput;

        public PlayerInputRouter()
        {
            _playerInput = new PlayerInput();
        }

        public void OnEnable()
        {
            _playerInput.Enable();
        }
        
        public void OnDisable()
        {
            _playerInput.Disable();
        }
        
        public Vector2 GetPointerPosition()
        {
            var x = _playerInput.Player.PointerPosition.ReadValue<Vector2>().x;
            var y = _playerInput.Player.PointerPosition.ReadValue<Vector2>().y;

            return new Vector2(x, y);
        }
    }
}