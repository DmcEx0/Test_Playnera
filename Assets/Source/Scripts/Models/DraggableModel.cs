using Cysharp.Threading.Tasks;
using Source.Scripts.View;

namespace Source.Scripts.Models
{
    //модель для связи между контроллерами
    public class DraggableModel : IDraggableModel
    {
        private readonly AsyncReactiveProperty<DraggableItemView> _draggable;
        public IAsyncReactiveProperty<DraggableItemView> Draggable => _draggable;

        public DraggableModel()
        {
            _draggable = new AsyncReactiveProperty<DraggableItemView>(null);
        }
        
        public void SetDraggable(DraggableItemView draggable)
        {
            _draggable.Value = draggable;
        }
    }
}