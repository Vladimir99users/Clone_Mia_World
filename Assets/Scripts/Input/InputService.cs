using System;
using System.Collections;
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
        public event Action<string> OnMovedScreenByObject;

        private bool? isObjectDraged = false;
        private Vector2 deltaScreenPosition;

        private Coroutine coroutine;
        private MonoBehaviour monoBehaviour;
        public void Initialize(MonoBehaviour monoBehaviour)
        {
            this.monoBehaviour = monoBehaviour;
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
            if (coroutine is not null)
                monoBehaviour.StopCoroutine(coroutine);
            isObjectDraged = false;
            OnEndDragObject?.Invoke();
        }

        private void Move(InputAction.CallbackContext obj)
        {
            var mousePosition = ReadMousePosition();
            if (coroutine is not null)
                monoBehaviour.StopCoroutine(coroutine);
            coroutine = monoBehaviour.StartCoroutine(MoveObject(mousePosition));
        }

        private IEnumerator MoveObject(Vector2 mousePosition)
        {
            // Добавил корутину, что бы не реализовывать ещё один метод Update
            // тут идёт перемещение взятого объекта и смещение сцены в нужную сторону
            while (isObjectDraged == true)
            {
                OnMovedObject?.Invoke(mousePosition);
                OnMovedScreenByObject?.Invoke(SideCheck(mousePosition));
                yield return null;
            }
        }

        private string SideCheck(Vector2 mousePosition)
        {
            // Проверка на то, если был взят объект, нужно ли его перемещать по сцене и кужа
            var width = Screen.width;
            var offset = Screen.width / 10;
            var leftSide = offset;
            var rightSide = width - offset;

            if (mousePosition.x < leftSide)
                return SideType.LeftSide;
            if (mousePosition.x > rightSide)
                return SideType.RightSide;

            return SideType.NoneSide;
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
}
