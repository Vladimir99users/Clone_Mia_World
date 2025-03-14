using UnityEngine;

namespace Assets.Scripts.Input
{
    //Интерфейс для манипулирования с объектом ( в данном случаи перемещение)
    public interface IStartManipulated
    {
        public void Manipulation(Vector2 vector);
    }
}