namespace Assets.Scripts.Input
{
    /*
     * Классы для расширения возможностей,
     * SideType -> класс для обозначений в какую сторону надо двигать экран
     * (не Enum, что бы расширять ( вверх или вниз перемещение (если потребуется)).
     *
     * LayerType ->  класс для того, что бы убрать магические символы из кода, так же
     * может расширяться, что бы в будущем к примеру, можно было бы складывать объекты
     * во внутрь других объектов.
     */
    public class SideType
    {
        public const string LeftSide = "Left";
        public const string RightSide = "Right";
        public const string NoneSide = "None";
    }

    public class LayerType
    {
        public const string GroundLayer = "Ground";
        public const string ObstacleLayer = "Obstacle";
    }
}