using UnityEngine;

namespace _Project.Scripts.Gameplay.Piece
{
    [CreateAssetMenu(fileName = "PieceParameters", menuName = "PuzzleGame/Piece Parameters", order = 3)]
    public class PieceParameters : ScriptableObject
    {
        [SerializeField, Min(1)] private int _angleStep = 90;
        [SerializeField] private int _targetAngle = 0;

        [Header("Анимация поворота")]
        [SerializeField, Min(0.01f)] private float _rotationDuration = 0.3f;
        [SerializeField, Min(1f)] private float _punchScale = 1.1f;

        public int AngleStep => _angleStep;
        public int TargetAngle => _targetAngle;
        public float RotationDuration => _rotationDuration;
        public float PunchScale => _punchScale;
    }
}
