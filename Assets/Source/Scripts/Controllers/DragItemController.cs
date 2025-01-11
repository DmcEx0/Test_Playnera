using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Source.Scripts.Models;
using VContainer.Unity;

namespace Source.Scripts.Controllers
{
    public class DragItemController : IInitializable, IDisposable
    {
        private readonly DraggableModel _draggableModel;
        private readonly PlayerInputModel _playerInputModel;

        private readonly CancellationTokenSource _tokenSource;

        public DragItemController(DraggableModel draggableModel, PlayerInputModel playerInputModel)
        {
            _draggableModel = draggableModel;
            _playerInputModel = playerInputModel;

            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _draggableModel.Draggable.Subscribe(MoveDraggableItemToPointer).AddTo(_tokenSource.Token);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource.Dispose();
        }
        
        private void MoveDraggableItemToPointer(IDraggable draggable)
        {
            if (draggable == null)
            {
                return;
            }

            var worldPosition = _playerInputModel.PointerWorldPosition;
            draggable.Drag(worldPosition);
        }
    }
}