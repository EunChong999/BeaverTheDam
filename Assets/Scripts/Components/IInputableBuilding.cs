using System.Collections;

public interface IInputableBuilding
{
    /// <summary>
    /// �������� ���̴� �Լ�
    /// </summary>
    void Input();

    /// <summary>
    /// ���� �������� ó���� ���� ����Ű�� �Լ�
    /// </summary>
    IEnumerator WaitForInput();
}
