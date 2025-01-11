using Cysharp.Threading.Tasks;

namespace Source.Scripts.Models
{
    public class DraggableModel
    {
        private readonly AsyncReactiveProperty<IDraggable> _draggable;
        
        public IReadOnlyAsyncReactiveProperty<IDraggable> Draggable => _draggable;

        public DraggableModel()
        {
            _draggable = new AsyncReactiveProperty<IDraggable>(null);
        }

        public void SetDraggable(IDraggable draggable)
        {
            _draggable.Value = draggable;
        }
    }
}