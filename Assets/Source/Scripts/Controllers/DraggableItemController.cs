using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Source.Scripts.Models;
using Source.Scripts.Utils;
using Source.Scripts.View;
using UnityEngine;
using VContainer.Unity;

namespace Source.Scripts.Controllers
{
    public class DraggableItemController : IInitializable, IDisposable
    {
        private readonly DraggableModel _draggableModel;
        private readonly PlayerInputModel _playerInputModel;
        private readonly DraggableItemHandler _draggableItemHandler;

        private readonly CancellationTokenSource _tokenSource;

        private DraggableItemView _currentDraggableItemView;
        private bool _isHaveDraggableItemView;

        public DraggableItemController(
            DraggableModel draggableModel,
            PlayerInputModel playerInputModel,
            DraggableItemHandler draggableItemHandler)
        {
            _draggableModel = draggableModel;
            _playerInputModel = playerInputModel;
            _draggableItemHandler = draggableItemHandler;

            _tokenSource = new CancellationTokenSource();
        }

        //скрипт отвечает за перемещение объектов, которые можно перетаскивать
        //используется реактивное свойство для определения, на какой предмет нажал игрок,
        //чтобы в дальнейшем им манипулировать
        public void Initialize()
        {
            _playerInputModel.PointerWorldPosition.Subscribe(DragItemToPointer).AddTo(_tokenSource.Token);
            _draggableModel.Draggable.Subscribe(ConfigureNewDraggable).AddTo(_tokenSource.Token);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource.Dispose();
        }

        private void DragItemToPointer(Vector3 pointerWorldPosition)
        {
            if (pointerWorldPosition == Vector3.zero || _isHaveDraggableItemView == false ||
                _playerInputModel.IsInteractionWithItem == false)
            {
                return;
            }

            _draggableItemHandler.Drag(_currentDraggableItemView, pointerWorldPosition);
        }

        //поскольку вызывается, когда игрок нажал на предмет и отпустил его,
        //вызываем методы для начала и окончания перетаскивания
        private void ConfigureNewDraggable(IDraggable draggable) 
        {
            if (_isHaveDraggableItemView)
            {
                _draggableItemHandler.EndDrag(_currentDraggableItemView);
            }

            CastingDraggableItemView(draggable);

            if (_isHaveDraggableItemView)
            {
                _draggableItemHandler.StartDrag(_currentDraggableItemView, _playerInputModel.StartPointerPosition);
            }
        }

        //кастим из интерфейса в конкретный тип
        private void CastingDraggableItemView(IDraggable draggable)
        {
            if (draggable is DraggableItemView draggableItemView)
            {
                _currentDraggableItemView = draggableItemView;
                _isHaveDraggableItemView = true;
                return;
            }

            _isHaveDraggableItemView = false;
        }
    }
}