using Assets.Scripts.Input;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    public class ItemController : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private LayerMask interectiveMask;
        private IManipulating manipulating;
        private IManipulated dragItem;
        public void Initialize(IManipulating manipulating)
        {
            this.manipulating = manipulating;
            Subscribe();
        }
        private void Subscribe()
        {
            manipulating.OnStartDragObject += DragObject;
            manipulating.OnEndDragObject += DropObject;
            manipulating.OnMovedObject += MovedObject;
        }



        private void OnDisable()
        {
            UnSubscribe();
        }
        private void UnSubscribe()
        {
            manipulating.OnStartDragObject -= DragObject;
            manipulating.OnEndDragObject -= DropObject;
            manipulating.OnMovedObject -= MovedObject;
        }

        private bool DragObject(Vector2 mousePosition)
        {
            RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(mousePosition), Vector2.zero, 50f, interectiveMask);

            if (hit.transform is null)
                return false;

            var gameObj = hit.transform.gameObject;
            if (gameObj.TryGetComponent<IStartManipulated>(out IStartManipulated startManipulated))
            {
                dragItem = startManipulated as IManipulated;
                return true;
            }

            return false;
        }
        private void DropObject()
        {
            if (dragItem is not null)
            {
                dragItem.EndManipulation();
                dragItem = null;
            }
        }
        private void MovedObject(Vector2 vector)
        {
            if (dragItem is null)
                return;

            dragItem.Manipulation(Camera.main.ScreenToWorldPoint(vector));
        }

        private void OnValidate()
        {
            if (camera is null)
                camera = Camera.main;
        }
    }
}
