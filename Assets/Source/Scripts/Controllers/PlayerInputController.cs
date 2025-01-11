using System;
using Source.Scripts.Models;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Source.Scripts.Controllers
{
    public class PlayerInputController : IInitializable, IDisposable, ITickable
    {
        private readonly PlayerInput _playerInput;
        private readonly DraggableModel _draggableModel;
        private readonly PlayerInputModel _playerInputModel;

        public PlayerInputController(DraggableModel draggableModel, PlayerInputModel playerInputModel)
        {
            _draggableModel = draggableModel;
            _playerInputModel = playerInputModel;
            
            _playerInput = new();
        }

        public void Initialize()
        {
            _playerInput.Player.PointerPress.canceled += OnPointerCanceled;
            _playerInput.Enable();
        }

        public void Dispose()
        {
            _playerInput.Player.PointerPress.canceled -= OnPointerCanceled;
            _playerInput.Disable();
            _playerInput?.Dispose();
        }

        public void Tick()
        {
            DefineItemOnClick();
        }

        private void DefineItemOnClick()
        {
            var isPressed = _playerInput.Player.PointerPress.IsPressed();
            
            if (isPressed == false)
            {
                return;
            }

            _playerInputModel.SetPressedValue(true);
            var collider = GetHitOnClick().collider;
            
            var worldPosition = GetCalculatedWorldPosition();
            _playerInputModel.SetPointerWorldPosition(worldPosition);

            if (collider == null)
            {
                return;
            }

            if (collider.TryGetComponent(out IDraggable draggable))
            {
                _draggableModel.SetDraggable(draggable);
                _playerInputModel.SetIsInteractionWithItemState(true);
            }
        }

        private void OnPointerCanceled(InputAction.CallbackContext ctx)
        {
            _playerInputModel.SetIsInteractionWithItemState(false);
            _draggableModel.SetDraggable(null);
            _playerInputModel.SetPressedValue(false);
        }

        private RaycastHit2D GetHitOnClick()
        {
            var worldPosition = GetCalculatedWorldPosition();
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            return hit;
        }

        private Vector3 GetCalculatedWorldPosition()
        {
            var pointerPosition = _playerInput.Player.PointerPosition.ReadValue<Vector2>();
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(pointerPosition);
            
            return worldPosition;
        }
    }
}