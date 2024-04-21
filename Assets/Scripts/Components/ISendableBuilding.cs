using System.Collections;
using UnityEngine;

public interface ISendableBuilding 
{
    /// <summary>
    /// 구면 경로의 중앙을 계산하는 함수
    /// </summary>
    IEnumerator GetCenter(Vector3 direction);

    /// <summary>
    /// 아이템을 구면 경로로 발사하는 함수
    /// </summary>
    IEnumerator ThrowItem(Transform item);
}
