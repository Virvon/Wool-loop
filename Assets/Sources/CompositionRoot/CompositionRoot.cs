using System;
using System.Collections.Generic;
using Sources.BaseLogic;
using Sources.BaseLogic.JointLogic;
using Sources.BaseLogic.RopeLogic;
using Sources.Infrastructure.JointModel;
using Sources.Services.InputService;
using UnityEngine;

namespace Sources.CompositionRoot
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private List<JointValidationData> _jointsDatas;
        [SerializeField] private JointListView _jointListView;
        [SerializeField] private Rope _ropePrefab;
        
        private IInputService _inputService;
        private Player _player;
        
        private void Start()
        {
            _inputService = new DesktopInputService();
            _player = new(_inputService, _camera, _ropePrefab);

            CreateJoints();
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
        
        private void CreateJoints()
        {
            JointsValidator jointsValidator = new(_jointsDatas);
            JointsValidatorPresentor presentor = new(jointsValidator, _jointListView);
        }
    }
}