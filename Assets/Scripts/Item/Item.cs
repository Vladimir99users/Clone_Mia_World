using UnityEngine;

namespace Assets.Scripts.Item
{
    //  [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] protected CircleCollider2D circleCollider;
        //public void Manipulation(Vector2 vector)
        //{
        //    Debug.Log($"Drag this object name {gameObject.name}");
        //    transform.position = vector;
        //    //  rb.gravityScale = 0;
        //    //  circleCollider.isTrigger = true;
        //}

        //public void EndManipulation()
        //{
        //    List<Collider2D> colliders = new List<Collider2D>();
        //    var amountCollider = Physics2D.OverlapCollider(circleCollider, new ContactFilter2D(), colliders);

        //    if (amountCollider != -1)
        //    {
        //        var firstCollider = colliders.FirstOrDefault();
        //    }
        //    Debug.Log($"Drop this object name {gameObject.name} amountCollider = {amountCollider}");
        //}




        private void OnValidate()
        {
            //if (rb is null)
            //{
            //    rb = GetComponent<Rigidbody2D>();
            //    rb.gravityScale = 0;
            //}
        }
    }
}
