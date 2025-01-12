using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Source.Scripts.Models;
using Source.Scripts.Utils;
using UnityEngine;
using VContainer.Unity;

namespace Source.Scripts.Controllers
{
    public class DraggableItemController : IInitializable, IDisposable
    {
        private readonly DraggableModel _draggableModel;
        private readonly PlayerInputModel _playerInputModel;
        private readonly DraggableItem _draggableItem;

        private readonly CancellationTokenSource _tokenSource;

        public DraggableItemController(DraggableModel draggableModel, PlayerInputModel playerInputModel)
        {
            _draggableModel = draggableModel;
            _playerInputModel = playerInputModel;

            _tokenSource = new CancellationTokenSource();

            _draggableItem = new DraggableItem();
        }

        public void Initialize()
        {
            _playerInputModel.PointerWorldPosition.Subscribe(DragItemToPointer).AddTo(_tokenSource.Token);
            // _playerInputModel.IsInteractionWithItem.Subscribe(ConfigureItemWithInteraction).AddTo(_tokenSource.Token);
            _draggableModel.Draggable.Subscribe(ConfigureNewDraggable).AddTo(_tokenSource.Token);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource.Dispose();
        }

        private void DragItemToPointer(Vector3 pointerWorldPosition)
        {
            if (_draggableModel.Draggable.Value == null || _playerInputModel.IsInteractionWithItem.Value == false)
            {
                return;
            }

            _draggableModel.NewDraggable.Drag(pointerWorldPosition);
        }

        // private void ConfigureItemWithInteraction(bool isInteractionWithItem)
        // {
        // }

        private void ConfigureNewDraggable(IDraggable draggable)
        {
            _ = _draggableItem.TrySetDraggableItem(draggable);
        }
    }
}