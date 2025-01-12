using Cysharp.Threading.Tasks;
using Source.Scripts.View;

namespace Source.Scripts.Models
{
    public interface IDraggableModel
    {
        public IAsyncReactiveProperty<IDraggable> Draggable { get; }

        public void SetDraggable(IDraggable draggable);
    }
}