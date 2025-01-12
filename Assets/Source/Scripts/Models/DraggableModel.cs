using Cysharp.Threading.Tasks;
using Source.Scripts.View;

namespace Source.Scripts.Models
{
    //модель для связи между контроллерами
    public class DraggableModel : IDraggableModel
    {
        private readonly AsyncReactiveProperty<IDraggable> _draggable;
        public IAsyncReactiveProperty<IDraggable> Draggable => _draggable;

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