namespace Assets.Scripts.Input
{
    /*
     * ������ ��� ���������� ������������,
     * SideType -> ����� ��� ����������� � ����� ������� ���� ������� �����
     * (�� Enum, ��� �� ��������� ( ����� ��� ���� ����������� (���� �����������)).
     *
     * LayerType ->  ����� ��� ����, ��� �� ������ ���������� ������� �� ����, ��� ��
     * ����� �����������, ��� �� � ������� � �������, ����� ���� �� ���������� �������
     * �� ������ ������ ��������.
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