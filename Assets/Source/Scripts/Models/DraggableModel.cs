using Cysharp.Threading.Tasks;
using Source.Scripts.View;

namespace Source.Scripts.Models
{
    public class DraggableModel
    {
        private readonly AsyncReactiveProperty<IDraggable> _newDraggable;
        public IDraggable NewDraggable { get; private set; }

        public IAsyncReactiveProperty<IDraggable> Draggable => _newDraggable;

        public DraggableModel()
        {
            _newDraggable = new AsyncReactiveProperty<IDraggable>(null);
        }
        
        public void SetDraggable(IDraggable draggable)
        {
            NewDraggable = draggable;
            Draggable.Value = draggable;
        }
    }
}