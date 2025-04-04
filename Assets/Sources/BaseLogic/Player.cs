using System;
using Sources.Services.InputService;
using UnityEngine;

namespace Sources.BaseLogic
{
    public class Player : IDisposable
    {
        private readonly IInputService _inputService;
        private readonly RopeCreator _ropeCreator;
        private readonly Camera _camera;
        
        private Rope _rope;

        public Player(IInputService inputService, RopeCreator ropeCreator, Camera camera)
        {
            _inputService = inputService;
            _ropeCreator = ropeCreator;
            _camera = camera;

            _inputService.ClickStarted += OnClickStarted;
            _inputService.Dragged += OnDragged;
        }

        public void Dispose()
        {
            _inputService.ClickStarted -= OnClickStarted;
            _inputService.Dragged -= OnDragged;
        }

        private void OnClickStarted(Vector2 position)
        {
            if (_rope == null)
                _rope = _ropeCreator.Create(ScreenToWorldPosition(position));
            
            //_currentRope.CreateSegment(_camera.ScreenToWorldPoint(new Vector3(position.x, position.y, 0)) + new Vector3(0, 2, 0));
        }

        private void OnDragged(Vector2 position)
        {
            _rope.Move(ScreenToWorldPosition(position));
        }

        private Vector2 ScreenToWorldPosition(Vector2 screenPosition)
        {
            return _camera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 1));
        }
    }
}