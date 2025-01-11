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

        private IDraggable _currentDraggableItem;
        private bool _isInteractionWithItem;

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
            MoveDraggableItemToPointer();
        }

        private void MoveDraggableItemToPointer()
        {
            if (_isInteractionWithItem == false)
            {
                return;
            }

            var worldPosition = GetCalculatedWorldPosition();
            _currentDraggableItem.Drag(worldPosition);
        }

        private void DefineItemOnClick()
        {
            if (_playerInput.Player.PointerPress.IsPressed() == false)
            {
                return;
            }

            var collider = GetHitOnClick().collider;

            if (collider == null)
            {
                return;
            }

            if (collider.TryGetComponent(out IDraggable draggable))
            {
                _draggableModel.SetDraggable(draggable);
                _playerInputModel.SetPointerWorldPosition(GetCalculatedWorldPosition());
                
                _currentDraggableItem = draggable;
                _isInteractionWithItem = true;
            }
        }

        private void OnPointerCanceled(InputAction.CallbackContext ctx)
        {
            _isInteractionWithItem = false;
            _currentDraggableItem = null;
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