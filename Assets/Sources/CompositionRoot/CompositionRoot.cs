using System;
using Sources.BaseLogic;
using Sources.Services.InputService;
using UnityEngine;

namespace Sources.CompositionRoot
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        private IInputService _inputService;
        private Player _player;
        
        private void Start()
        {
            _inputService = new DesktopInputService();
            RopeCreator ropeCreator = new();
            _player = new(_inputService, ropeCreator, _camera);
        }

        private void OnDestroy()
        {
            _player.Dispose();
        }

        private void Update()
        {
            if(_inputService != null)
                _inputService.Tick();
        }
    }
}