using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Source.Scripts.Models;
using UnityEngine;
using VContainer.Unity;

namespace Source.Scripts.Controllers
{
    public class DraggableItemController : IInitializable, IDisposable
    {
        private readonly DraggableModel _draggableModel;
        private readonly PlayerInputModel _playerInputModel;

        private readonly CancellationTokenSource _tokenSource;

        public DraggableItemController(DraggableModel draggableModel, PlayerInputModel playerInputModel)
        {
            _draggableModel = draggableModel;
            _playerInputModel = playerInputModel;

            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _playerInputModel.PointerWorldPosition.Subscribe(MoveItemToPointer).AddTo(_tokenSource.Token);
            _playerInputModel.IsInteractionWithItem.Subscribe(ConfigureItemOnInteraction).AddTo(_tokenSource.Token);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource.Dispose();
        }

        private void MoveItemToPointer(Vector3 pointerWorldPosition)
        {
            if(pointerWorldPosition == Vector3.zero || _draggableModel.Draggable == null ||
               _playerInputModel.IsInteractionWithItem.Value == false)
            {
                return;
            }

            _draggableModel.Draggable.Drag(pointerWorldPosition);
        }

        private void ConfigureItemOnInteraction(bool isInteractionWithItem)
        {
            if(_draggableModel.Draggable == null)
            {
                return;
            }
            
            if (isInteractionWithItem)
            {
                _draggableModel.Draggable.Rb.bodyType = RigidbodyType2D.Kinematic;
                
                return;
            }
            
            _draggableModel.Draggable.Rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}