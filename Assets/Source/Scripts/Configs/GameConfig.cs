using UnityEngine;

namespace Source.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Configs/GameConfig", fileName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float DragSpeed { get; private set; }
        [field: SerializeField] public float ItemScaleOnDrag { get; private set; }
        [field: SerializeField] public float ItemMinScale { get; private set; }
        [field: SerializeField] public float ItemAnimationDurationOnDrag { get; private set; }
        [field: SerializeField] public float ItemAnimationDurationAfterDrag { get; private set; }
        [field: SerializeField] public LayerMask ItemLayerMask { get; private set; }
    }
}