using System;
using UnityEngine;

namespace Sources.Services.InputService
{
    public interface IInputService
    {
        event Action<Vector2> ClickStarted; 
        event Action ClickEnded;
        event Action<Vector2> Dragged;

        void Tick();
    }
}