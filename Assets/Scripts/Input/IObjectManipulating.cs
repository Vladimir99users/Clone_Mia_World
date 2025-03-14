using System;
using UnityEngine;

namespace Assets.Scripts.Input
{
    public interface IObjectManipulating
    {
        public event Action<Vector2> OnMovedObject;
        public event Func<Vector2, bool> OnStartDragObject;
        public event Action OnEndDragObject;
    }
}