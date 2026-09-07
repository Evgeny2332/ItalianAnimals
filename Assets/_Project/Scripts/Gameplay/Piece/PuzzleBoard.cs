using System;
using System.Collections.Generic;

namespace _Project.Scripts.Gameplay.Piece
{
    public sealed class PuzzleBoard : IDisposable
    {
        private readonly IReadOnlyList<PuzzlePiece> _pieces;
        private readonly IReadOnlyList<IDisposable> _bindings;

        private bool _isCompleted;

        public event Action Completed;

        public PuzzleBoard(IReadOnlyList<PuzzlePiece> pieces, IReadOnlyList<IDisposable> bindings)
        {
            _pieces = pieces;
            _bindings = bindings;

            foreach (var piece in _pieces)
                piece.Rotated += OnPieceRotated;
        }

        public void Dispose()
        {
            foreach (var piece in _pieces)
                piece.Rotated -= OnPieceRotated;

            foreach (var binding in _bindings)
                binding.Dispose();
        }

        private void OnPieceRotated(int _)
        {
            if (_isCompleted || !IsSolved())
                return;

            _isCompleted = true;
            Completed?.Invoke();
        }

        private bool IsSolved()
        {
            foreach (var piece in _pieces)
            {
                if (!piece.IsSolved)
                    return false;
            }

            return true;
        }
    }
}
