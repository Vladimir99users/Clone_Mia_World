using System;
using UnityEngine;

namespace Assets.Scripts.Input
{
    // Ёкран дл€ манипул€ций
    public interface IScreenManipulating
    {
        public event Action<Vector2> OnMovedScreen;
        public event Action<string> OnMovedScreenByObject;

    }
}