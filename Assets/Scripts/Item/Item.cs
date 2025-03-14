using UnityEngine;

namespace Assets.Scripts.Item
{
    //  [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] protected CircleCollider2D circleCollider;


        private void OnValidate()
        {
            CheckValidate();
        }

        protected virtual void CheckValidate()
        {

        }
    }
}
