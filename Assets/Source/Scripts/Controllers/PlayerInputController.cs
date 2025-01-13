using System;
using Source.Scripts.Configs;
using Source.Scripts.Models;
using Source.Scripts.View;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Source.Scripts.Controllers
{
    public class PlayerInputController : IInitializable, IDisposable, ITickable
    {
        private const float RayDistance = 100f;

        private readonly PlayerInput _playerInput;
        private readonly IDraggableModel _draggableModel;
        private readonly IPlayerInputModel _playerInputModel;
        private readonly Camera _camera;
        private readonly GameConfig _gameConfig;

        public PlayerInputController(
            IDraggableModel draggableModel,
            IPlayerInputModel playerInputModel,
            Camera camera,
            GameConfig gameConfig)
        {
            _draggableModel = draggableModel;
            _playerInputModel = playerInputModel;
            _camera = camera;
            _gameConfig = gameConfig;

            _playerInput = new();
        }
        
        //скрипт для обработки ввода игрока с использованием New Input System. Класс PlayerInput - автогенерируемый
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

        // проверка нажатия поинтера, если нажата - определение позиции поинтера в мировых координатах
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
            
            if (collider.TryGetComponent(out DraggableItemView draggable)) // обработка нажатия на предмет 
            {
                //определение позиции поинтера в момент клика, для корректного перемещения предмета, относительно поинтера
                _playerInputModel.SetStartPointerPosition(GetCalculatedWorldPosition()); 

                _draggableModel.SetDraggable(draggable);
                _playerInputModel.SetIsInteractionWithItemState(true);
            }
        }

        //устанавливаем значения, для вызовов реактивных свойств
        private void OnPointerCanceled(InputAction.CallbackContext ctx)
        {
            _playerInputModel.SetPointerWorldPosition(Vector3.zero);
            _playerInputModel.SetIsInteractionWithItemState(false);
            _draggableModel.SetDraggable(null);
        }

        private RaycastHit2D GetHitOnClick()
        {
            var worldPosition = GetCalculatedWorldPosition();
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, RayDistance,
                _gameConfig.ItemLayerMask);

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