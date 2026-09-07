using System;
using System.Collections.Generic;
using _Project.Scripts.Core;
using _Project.Scripts.Services.Audio;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Piece
{
    public sealed class PuzzleBoardFactory
    {
        private const int FullCircle = 360;

        private readonly PieceParameters _pieceParameters;
        private readonly IAudioService _audioService;

        public PuzzleBoardFactory(PieceParameters pieceParameters, IAudioService audioService)
        {
            _pieceParameters = pieceParameters;
            _audioService = audioService;
        }

        public PuzzleBoard Create(IReadOnlyList<PuzzlePieceView> views, LevelConfig levelConfig)
        {
            IReadOnlyList<Sprite> sprites = levelConfig.PieceSprites;

            if (sprites.Count != views.Count)
                Debug.LogWarning($"Кусочков на сцене {views.Count}, а спрайтов в конфиге {sprites.Count}.");

            int count = Mathf.Min(views.Count, sprites.Count);

            var pieces = new List<PuzzlePiece>(count);
            var bindings = new List<IDisposable>(count);

            for (int i = 0; i < count; i++)
            {
                var piece = new PuzzlePiece(_pieceParameters.AngleStep, _pieceParameters.TargetAngle, GetRandomStartAngle());
                var view = views[i];

                view.SetIcon(sprites[i]);
                view.SetAnimationSettings(_pieceParameters.RotationDuration, _pieceParameters.PunchScale);

                pieces.Add(piece);
                bindings.Add(new PuzzlePiecePresenter(piece, view, _audioService));
            }

            return new PuzzleBoard(pieces, bindings);
        }

        private int GetRandomStartAngle()
        {
            int steps = Mathf.Max(2, FullCircle / _pieceParameters.AngleStep);
            return _pieceParameters.TargetAngle + Random.Range(1, steps) * _pieceParameters.AngleStep;
        }
    }
}
