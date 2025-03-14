using Assets.Scripts.Input;
using UnityEngine;

namespace Assets.Scripts.Item
{
    public class DragItem : Item, IManipulated
    {
        [SerializeField] private Rigidbody2D rb2D;

        public void Manipulation(Vector2 vector)
            => Drag(vector);
        public void EndManipulation()
            => Drop();
        private void Drag(Vector2 vector)
        {
            Debug.Log($"Drag this object name {gameObject.name}");
            transform.position = vector;
            rb2D.gravityScale = 0;
        }
        private void Drop()
            => rb2D.gravityScale = 1;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            var enterLayerName = LayerMask.LayerToName(collider.transform.gameObject.layer);
            if (IsCorrectLayer(enterLayerName))
                rb2D.bodyType = RigidbodyType2D.Static;
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            var exitLayerName = LayerMask.LayerToName(collider.transform.gameObject.layer);
            if (IsCorrectLayer(exitLayerName))
                rb2D.bodyType = RigidbodyType2D.Dynamic;
        }

        private bool IsCorrectLayer(string layerName)
            => layerName == "Ground" || layerName == "Obstacle";

        protected override void CheckValidate()
        {
            base.CheckValidate();
            if (rb2D is null)
            {
                rb2D = GetComponent<Rigidbody2D>();
                rb2D.bodyType = RigidbodyType2D.Static;
            }
        }
    }
}