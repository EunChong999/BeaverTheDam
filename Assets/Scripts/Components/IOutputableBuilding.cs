using System.Collections;

public interface IOutputableBuilding
{
    /// <summary>
    /// �������� �������� �Լ�
    /// </summary>
    void Output();

    /// <summary>
    /// ������ �������� ó���� ���� ����Ű�� �Լ�
    /// </summary>
    IEnumerator WaitForOutput();
}
