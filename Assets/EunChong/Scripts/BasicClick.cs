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

    // �̹�Ʈ �Լ� ���
    #region OnMouseDown

    /// <summary>
    /// ��ǥ ȸ������ �����ϴ� �Լ�
    /// </summary>
    public void SetTargetRotation()
    {
        targetRotation += 90;
    }

    /// <summary>
    /// ���͸����� ��ȯ��Ű�� �Լ�
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
    /// Y���� ȸ������ 360���� �Ѿ��� ���,
    /// <br></br>
    /// Y���� ȸ������ 0���� �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    private void ResetRotation()
    {
        if (Mathf.CeilToInt(transform.eulerAngles.y) >= 355)
        {
            targetRotation = 0;
        }
    }

    /// <summary>
    /// Transform�� ȸ����Ű�� �Լ�
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
