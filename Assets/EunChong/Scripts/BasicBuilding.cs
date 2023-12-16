using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBuilding : MonoBehaviour
{
    [SerializeField] float rotationTime;
    [SerializeField] Ease moveEase;
    [SerializeField] Ease scaleEase;

    bool isRotating;
    Sequence scaleSequence;
    string str;

    /// <summary>
    /// ī�޶� �ٶ󺸴� �Լ�
    /// </summary>
    public void LookCameraRotation()
    {
        foreach (Transform t in transform)
        {
            t.LookAt(t.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }

    /// <summary>
    /// ȸ���� Ʈ���� ȿ���� �ִ� �Լ�
    /// </summary>
    public void ShowEffect()
    {

    }

    /// <summary>
    /// ȸ������ ���� ��������Ʈ�� �����ϴ� �Լ�
    /// </summary>
    public void ChangeSprite()
    {
        if ((int)transform.eulerAngles.y >= 0 && (int)transform.eulerAngles.y < 90) 
        { transform.Find("ConveyorBelt_0 (0)").gameObject.SetActive(true); }
        else { transform.Find("ConveyorBelt_0 (0)").gameObject.SetActive(false); }

        if ((int)transform.eulerAngles.y >= 90 && (int)transform.eulerAngles.y < 180) 
        { transform.Find("ConveyorBelt_0 (1)").gameObject.SetActive(true); }
        else { transform.Find("ConveyorBelt_0 (1)").gameObject.SetActive(false); }

        if ((int)transform.eulerAngles.y >= 180 && (int)transform.eulerAngles.y < 270) 
        { transform.Find("ConveyorBelt_0 (2)").gameObject.SetActive(true); }
        else { transform.Find("ConveyorBelt_0 (2)").gameObject.SetActive(false); }

        if ((int)transform.eulerAngles.y >= 270 && (int)transform.eulerAngles.y < 360) 
        { transform.Find("ConveyorBelt_0 (3)").gameObject.SetActive(true); }
        else { transform.Find("ConveyorBelt_0 (3)").gameObject.SetActive(false); }
    }

    /// <summary>
    /// Transform�� ȸ����Ű�� �Լ�
    /// </summary>
    public void RotateTransform()
    {
        if (!isRotating)
        {
            Vector3 targetRotation = transform.eulerAngles + new Vector3(0, 90, 0);
            transform.DORotate(targetRotation, rotationTime).SetEase(moveEase);

            Invoke(nameof(ApplyChangedValue), rotationTime);

            isRotating = true;
        }
    }

    /// <summary>
    /// ȸ���� ���� ���� ���� �����ϴ� �Լ�
    /// </summary>
    private void ApplyChangedValue()
    {
        isRotating = false;
    }
}
