using System;
using _Project.Scripts.Services.Audio;

namespace _Project.Scripts.Gameplay.Piece
{
    public sealed class PuzzlePiecePresenter : IDisposable
    {
        private readonly PuzzlePiece _piece;
        private readonly PuzzlePieceView _view;
        private readonly IAudioService _audioService;

        public PuzzlePiecePresenter(PuzzlePiece piece, PuzzlePieceView view, IAudioService audioService)
        {
            _piece = piece;
            _view = view;
            _audioService = audioService;

            _view.SetAngle(_piece.CurrentAngle);

            _piece.Rotated += OnPieceRotated;
            _view.Clicked += OnViewClicked;
        }

        public void Dispose()
        {
            _piece.Rotated -= OnPieceRotated;
            _view.Clicked -= OnViewClicked;
        }

        private void OnViewClicked() =>
            _piece.Rotate();

        private void OnPieceRotated(int deltaAngle)
        {
            _view.PlayRotation(deltaAngle);
            _audioService.Play(SoundId.PieceRotate);
        }
    }
}
