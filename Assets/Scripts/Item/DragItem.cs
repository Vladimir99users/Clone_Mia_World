using Assets.Scripts.Input;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Item
{
    public class DragItem : Item, IManipulated
    {
        public void Manipulation(Vector2 vector)
        {
            Drag(vector);
        }

        public void EndManipulation()
        {
            Drop();
        }

        private void Drag(Vector2 vector)
        {

            Debug.Log($"Drag this object name {gameObject.name}");
            transform.position = vector;
        }

        private void Drop()
        {
            List<Collider2D> colliders = new List<Collider2D>();
            var amountCollider = Physics2D.OverlapCollider(circleCollider, new ContactFilter2D(), colliders);

            if (amountCollider != -1)
            {
                var firstCollider = colliders.FirstOrDefault();
            }

            Debug.Log($"Drop this object name {gameObject.name} amountCollider = {amountCollider}");
        }
    }
}