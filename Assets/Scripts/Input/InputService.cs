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
        public event Action<bool> OnMovedScreenByObject;

        private bool? isObjectDraged = false;
        private Vector2 deltaScreenPosition;

        private Coroutine coroutine;

        public void Initialize()
        {
            playerInput = new PlayerInputAction();
            playerInput.Enable();

            playerInput.Player.TouchClick.started += Drag;
            playerInput.Player.TouchClick.canceled += Drop;
            playerInput.Player.Move.performed += Move;

            playerInput.Player.DeltaScreen.performed += MoveStartScreen;
            playerInput.Player.DeltaScreen.canceled += MoveEndScreen;
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
            {
                var onClick = obj.ReadValueAsButton();
                Debug.Log(onClick);
                OnMovedObject?.Invoke(mousePosition);
                OnMovedScreenByObject?.Invoke(SideCheck(mousePosition));
            }
        }

        private bool SideCheck(Vector2 mousePosition)
        {
            var width = Screen.width;
            var offset = Screen.width / 10;
            var leftSide = offset;
            var rightSide = width - offset;
            return mousePosition.x < leftSide || mousePosition.x > rightSide;
        }



        private void MoveStartScreen(InputAction.CallbackContext obj)
        {
            if (isObjectDraged == true)
                return;

            deltaScreenPosition = obj.ReadValue<Vector2>();
            OnMovedScreen?.Invoke(deltaScreenPosition);
        }

        private void MoveEndScreen(InputAction.CallbackContext obj)
        {
            OnMovedScreen?.Invoke(Vector2.zero);
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
        public event Action<bool> OnMovedScreenByObject;
        public event Action<Vector2> OnMovedObject;
        public event Func<Vector2, bool> OnStartDragObject;
        public event Action OnEndDragObject;
    }
}
