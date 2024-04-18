using System.Collections;
using UnityEngine;

public interface ISendableBuilding 
{
    IEnumerator GetCenter(Vector3 direction);
    IEnumerator ThrowItem(Transform item);
}
