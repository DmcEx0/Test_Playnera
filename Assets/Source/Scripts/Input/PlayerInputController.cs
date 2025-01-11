using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Scripts.Input
{
    public class PlayerInputController
    {
        private readonly PlayerInput _playerInput;

        public PlayerInputController()
        {
            _playerInput = new PlayerInput();
        }

        public void OnEnable()
        {
            _playerInput.Enable();
            _playerInput.Player.PointerPress.performed += OnClick;
        }
        
        public void OnDisable()
        {
            _playerInput.Disable();
            _playerInput.Player.PointerPress.performed -= OnClick;
        }
        
        private void OnClick(InputAction.CallbackContext ctx)
        {
            var collider = GetHitOnClick().collider;
            
            if (collider == null)
            {
                return;
            }
            
            if(collider.TryGetComponent(out IDraggable draggable))
            {
                var x = _playerInput.Player.PointerPosition.ReadValue<Vector2>().x;
                var y = _playerInput.Player.PointerPosition.ReadValue<Vector2>().y;

                draggable.Drag(new Vector2(x, y));
            }
        }

        private RaycastHit2D GetHitOnClick()
        {
            var pointerPosition = _playerInput.Player.PointerPress.ReadValue<Vector2>();
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(pointerPosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            return hit;
        }
    }
}