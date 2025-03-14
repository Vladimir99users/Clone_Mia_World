using System;
using UnityEngine;

namespace Assets.Scripts.Input
{
    public interface IScreenManipulating
    {
        public event Action<Vector2> OnMovedScreen;
        public event Action<string> OnMovedScreenByObject;

    }
}