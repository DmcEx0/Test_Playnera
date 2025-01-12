using System;
using Source.Scripts.Models;
using Source.Scripts.View;
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
        private readonly Camera _camera;

        public PlayerInputController(DraggableModel draggableModel, PlayerInputModel playerInputModel, Camera camera)
        {
            _draggableModel = draggableModel;
            _playerInputModel = playerInputModel;
            _camera = camera;
            
            _playerInput = new();
        }

        public void Initialize()
        {
            _playerInput.Player.PointerPress.started += OnPointerStarted;
            _playerInput.Player.PointerPress.canceled += OnPointerCanceled;
            
            _playerInput.Enable();
        }

        public void Dispose()
        {
            _playerInput.Player.PointerPress.started -= OnPointerStarted;
            _playerInput.Player.PointerPress.canceled -= OnPointerCanceled;
            
            _playerInput.Disable();
        }

        public void Tick()
        {
            DefinePointerPositionOnPressed();
        }

        private void DefinePointerPositionOnPressed()
        {
            if (_playerInput.Player.PointerPress.IsPressed() == false)
            {
                return;
            }
            
            var worldPosition = GetCalculatedWorldPosition();
            _playerInputModel.SetPointerWorldPosition(worldPosition);
        }
        
        private void OnPointerStarted(InputAction.CallbackContext ctx)
        {
            var collider = GetHitOnClick().collider;

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
            _playerInputModel.SetPointerWorldPosition(Vector3.zero);
            _playerInputModel.SetIsInteractionWithItemState(false);
            _draggableModel.SetDraggable(null);
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
            Vector3 worldPosition = _camera.ScreenToWorldPoint(pointerPosition);
            
            return worldPosition;
        }
    }
}