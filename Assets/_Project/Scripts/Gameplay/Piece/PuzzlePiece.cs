using System;

namespace _Project.Scripts.Gameplay.Piece
{
    public sealed class PuzzlePiece
    {
        private const int FullCircle = 360;

        private readonly int _angleStep;
        private readonly int _targetAngle;

        public int CurrentAngle { get; private set; }
        public bool IsSolved => Normalize(CurrentAngle) == Normalize(_targetAngle);

        public event Action<int> Rotated;

        public PuzzlePiece(int angleStep, int targetAngle, int startAngle)
        {
            _angleStep = angleStep;
            _targetAngle = targetAngle;
            CurrentAngle = Normalize(startAngle);
        }

        public void Rotate()
        {
            CurrentAngle = Normalize(CurrentAngle + _angleStep);
            Rotated?.Invoke(_angleStep);
        }

        private static int Normalize(int angle) =>
            ((angle % FullCircle) + FullCircle) % FullCircle;
    }
}
