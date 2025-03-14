using Assets.Scripts.Input;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    // скрипт для управления сценой ( а именно перемещение по оси Х c ограничением по сторонам.
    // в будущем можно сделать так, что бы данный контроллер считывал размер карты.
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float step;
        [SerializeField] private Camera camera;
        [SerializeField] private float leftEndPosition;
        [SerializeField] private float rightEndPosition;
        private IScreenManipulating manipulating;
        private Vector2 movingVector;
        private string isMoveStep = SideType.NoneSide;
        public void Initialize(IScreenManipulating manipulating)
        {
            this.manipulating = manipulating;
            this.manipulating.OnMovedScreen += MovedScreen;
            this.manipulating.OnMovedScreenByObject += MoveScreenByStep;
        }

        private void MovedScreen(Vector2 obj)
            => movingVector = obj;
        private void MoveScreenByStep(string side)
            => isMoveStep = side;

        private void Update()
        {
            if (isMoveStep == SideType.RightSide)
            {
                float offset = camera.transform.position.x + step * Time.deltaTime;
                Move(offset);
            }
            else if (isMoveStep == SideType.LeftSide)
            {
                float offset = camera.transform.position.x - step * Time.deltaTime;
                Move(offset);
            }
            else
            {
                if (movingVector.Equals(Vector2.zero))
                    return;

                float offset = camera.transform.position.x - movingVector.x * speed * Time.deltaTime;
                Move(offset);
            }
        }
        private void Move(float offset)
        {
            camera.transform.position = new Vector3(offset, camera.transform.position.y, camera.transform.position.z);
            var x = ClampScreen(camera.transform.position.x);
            camera.transform.position = new Vector3(x, camera.transform.position.y, camera.transform.position.z);
        }

        private float ClampScreen(float x)
            => Mathf.Clamp(x, leftEndPosition, rightEndPosition);





        private void OnValidate()
        {
            if (camera is null)
                camera = Camera.main;
        }
    }
}