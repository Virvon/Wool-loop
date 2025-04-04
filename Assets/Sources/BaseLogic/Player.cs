using System;
using Sources.BaseLogic.RopeLogic;
using Sources.Infrastructure.RopeModel;
using Sources.Services.InputService;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.BaseLogic
{
    public class Player : IDisposable
    {
        private readonly IInputService _inputService;
        private readonly Camera _camera;
        private readonly Rope _ropePrefab;
        
        private RopeModel _rope;
        private RopePresentor _ropePresentor;

        public Player(IInputService inputService, Camera camera, Rope ropePrefab)
        {
            _inputService = inputService;
            _camera = camera;
            _ropePrefab = ropePrefab;

            _inputService.ClickStarted += OnClickStarted;
            _inputService.Dragged += OnDragged;
        }

        public void Dispose()
        {
            _inputService.ClickStarted -= OnClickStarted;
            _inputService.Dragged -= OnDragged;
            
            _ropePresentor.Dispose();
        }

        private void OnClickStarted(Vector2 position)
        {
            if (_rope == null)
                CreateRope(_camera.ScreenToWorldPoint(position));

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

        private void CreateRope(Vector2 position)
        {
            Rope rope = Object.Instantiate(_ropePrefab, position, Quaternion.identity);
            _rope = new RopeModel(position);
            _ropePresentor = new RopePresentor(_rope, rope);
        }
    }
}