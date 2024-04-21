using System.Collections;
using UnityEngine;

public interface ISendableBuilding 
{
    /// <summary>
    /// ���� ����� �߾��� ����ϴ� �Լ�
    /// </summary>
    IEnumerator GetCenter(Vector3 direction);

    /// <summary>
    /// �������� ���� ��η� �߻��ϴ� �Լ�
    /// </summary>
    IEnumerator ThrowItem(Transform item);
}
