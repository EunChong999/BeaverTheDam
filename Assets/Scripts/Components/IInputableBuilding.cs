using System.Collections;

public interface IInputableBuilding
{
    /// <summary>
    /// �������� ���̴� �Լ�
    /// </summary>
    IEnumerator Input();

    /// <summary>
    /// ���� �������� ó���� ���� ����Ű�� �Լ�
    /// </summary>
    IEnumerator WaitForInput();
}
