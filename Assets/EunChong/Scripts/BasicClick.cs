using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicClick : MonoBehaviour
{
    [SerializeField] Material fromMaterial;
    [SerializeField] Material toMaterial;

    [SerializeField] float targetRotation;
    [SerializeField] float rotationSpeed;

    [SerializeField] bool isRotating;

    // 이번트 함수 목록
    #region OnMouseDown

    /// <summary>
    /// 목표 회전값을 설정하는 함수
    /// </summary>
    public void SetTargetRotation()
    {
        targetRotation += 90;
    }

    /// <summary>
    /// 머터리얼을 전환시키는 함수
    /// </summary>
    public void ConvertMaterial()
    {
        if (gameObject.GetComponent<MeshRenderer>().sharedMaterial == fromMaterial)
        {
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = toMaterial;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = fromMaterial;
        }
    }

    #endregion
    #region Update

    /// <summary>
    /// Y축의 회전각이 360도를 넘었을 경우,
    /// <br></br>
    /// Y축의 회전각을 0도로 초기화하는 함수
    /// </summary>
    private void ResetRotation()
    {
        if (Mathf.CeilToInt(transform.eulerAngles.y) >= 355)
        {
            targetRotation = 0;
        }
    }

    /// <summary>
    /// Transform을 회전시키는 함수
    /// </summary>
    private void RotateTransform()
    {
        if (transform.eulerAngles.y >= targetRotation)
        {
            isRotating = false;

            transform.eulerAngles = new Vector3(0, targetRotation, 0);
        }
        else
        {
            isRotating = true;

            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }

    #endregion

    private void OnMouseDown()
    {
        if (!isRotating) 
        {
            SetTargetRotation();
            ConvertMaterial();
        }
    }

    private void Update()
    {
        ResetRotation();
        RotateTransform();
    }
}
