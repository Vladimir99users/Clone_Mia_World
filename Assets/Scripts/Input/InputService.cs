using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Input
{
    public class InputService : IManipulating
    {
        private PlayerInputAction playerInput;
        public event Func<Vector2, bool> OnStartDragObject;
        public event Action OnEndDragObject;
        public event Action<Vector2> OnMovedScreen;
        public event Action<Vector2> OnMovedObject;

        private bool? isObjectDraged = false;
        public void Initialize()
        {
            playerInput = new PlayerInputAction();
            playerInput.Enable();

            playerInput.Player.TouchClick.started += Drag;
            playerInput.Player.TouchClick.canceled += Drop;
            playerInput.Player.Move.performed += Move;
        }

        private void Drag(InputAction.CallbackContext obj)
        {
            var mousePosition = ReadMousePosition();
            isObjectDraged = OnStartDragObject?.Invoke(mousePosition);
        }
        private void Drop(InputAction.CallbackContext obj)
        {
            isObjectDraged = false;
            OnEndDragObject?.Invoke();
        }

        private void Move(InputAction.CallbackContext obj)
        {
            var mousePosition = ReadMousePosition();
            if (isObjectDraged == true)
                OnMovedObject?.Invoke(mousePosition);
            else
                OnMovedScreen?.Invoke(mousePosition);
        }

        private Vector2 ReadMousePosition()
            => playerInput.Player.Move.ReadValue<Vector2>();

    }


    public interface IManipulated : IStartManipulated, IEndManipulated
    {
    }

    public interface IStartManipulated
    {
        public void Manipulation(Vector2 vector);
    }

    public interface IEndManipulated
    {
        public void EndManipulation();
    }


    public interface IManipulating
    {
        public event Action<Vector2> OnMovedScreen;
        public event Action<Vector2> OnMovedObject;
        public event Func<Vector2, bool> OnStartDragObject;
        public event Action OnEndDragObject;
    }
}
