namespace Assets.Scripts.Input
{
    //Интерфейс для обозначения того, что может и начинать манипулировать с объектами
    // и заканчивать
    public interface IManipulated : IStartManipulated, IEndManipulated
    {
    }
}