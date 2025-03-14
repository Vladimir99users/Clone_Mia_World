using UnityEngine;

namespace Assets.Scripts.Item
{
    // Объект на сцене
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] protected CircleCollider2D circleCollider;
        private void OnValidate()
            => CheckValidate();
        protected virtual void CheckValidate() { }
    }
}
