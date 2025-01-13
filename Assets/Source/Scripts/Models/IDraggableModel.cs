using Cysharp.Threading.Tasks;
using Source.Scripts.View;

namespace Source.Scripts.Models
{
    public interface IDraggableModel
    {
        public IAsyncReactiveProperty<DraggableItemView> Draggable { get; }

        public void SetDraggable(DraggableItemView draggable);
    }
}