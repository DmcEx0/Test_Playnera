using Source.Scripts.Models;

namespace Source.Scripts.Controllers
{
    public class DragItemController
    {
        private readonly DraggableModel _draggableModel;

        public DragItemController(DraggableModel draggableModel)
        {
            _draggableModel = draggableModel;
        }
    }
}