using Assets.Scripts.Input;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float step;
        [SerializeField] private Camera camera;
        [SerializeField] private float leftEndPosition;
        [SerializeField] private float rightEndPosition;
        private IManipulating manipulating;
        private Vector2 movingVector;
        private bool isMoveStep = false;
        public void Initialize(IManipulating manipulating)
        {
            this.manipulating = manipulating;
            this.manipulating.OnMovedScreen += MovedScreen;
            this.manipulating.OnMovedScreenByObject += MoveScreenByStep;
        }

        private void MovedScreen(Vector2 obj)
        {
            float deltaX = obj.x;
            //  Debug.Log(deltaX);
            movingVector = obj;

        }


        private void Update()
        {
            if (isMoveStep)
            {
                camera.transform.position = new Vector3(camera.transform.position.x + step * Time.deltaTime,
                    camera.transform.position.y,
                    camera.transform.position.z);

                var x = ClampScreen(camera.transform.position.x);
                camera.transform.position = new Vector3(x, camera.transform.position.y, camera.transform.position.z);
            }
            else
            {
                if (movingVector.Equals(Vector2.zero))
                    return;

                Move();
            }
        }

        private void Move()
        {
            float deltaX = movingVector.x;
            camera.transform.position = new Vector3(camera.transform.position.x - deltaX * speed * Time.deltaTime,
                camera.transform.position.y,
                camera.transform.position.z);

            var x = ClampScreen(camera.transform.position.x);
            camera.transform.position = new Vector3(x, camera.transform.position.y, camera.transform.position.z);
        }

        private float ClampScreen(float x)
            => Mathf.Clamp(x, leftEndPosition, rightEndPosition);


        private void MoveScreenByStep(bool isCheck)
        {
            isMoveStep = isCheck;
            // Debug.Log(isMoveStep);
        }


        private void OnValidate()
        {
            if (camera is null)
                camera = Camera.main;
        }
    }
}