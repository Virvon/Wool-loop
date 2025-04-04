using System;
using UnityEngine;

namespace Sources.Services.InputService
{
    public class DesktopInputService : IInputService
    {
        private bool _isClickStarted;
        private bool _isDragged;

        private Vector3 _clickStartPosition;
        
        public event Action ClickEnded;
        public event Action<Vector2> Dragged;
        public event Action<Vector2> ClickStarted; 

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _clickStartPosition = Input.mousePosition;
                _isClickStarted = true;
                ClickStarted?.Invoke(_clickStartPosition);
            }

            if (Input.GetMouseButton(0) && _isClickStarted)
                Dragged?.Invoke(Input.mousePosition);

            if (Input.GetMouseButtonUp(0) && _clickStartPosition == Input.mousePosition)
            {
                ClickEnded?.Invoke();
                _isClickStarted = false;
            }
        }
    }
}