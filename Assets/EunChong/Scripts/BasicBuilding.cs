using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBuilding : MonoBehaviour
{
    #region Variables
    [Header("BasicBuilding")]

    [Space(10)]

    public float rotationTime;

    [SerializeField] Ease rotationEase;
    [SerializeField] float startScaleTime;
    [SerializeField] float endScaleTime;
    [SerializeField] Ease startScaleEase;
    [SerializeField] Ease endScaleEase;

    [HideInInspector] public bool isRotating;
    [HideInInspector] public Vector3 originScale;

    Sequence scaleSequence;
    #endregion

    #region Functions
    /// <summary>
    /// ȸ���� ���� ��ü���� ������ �����ϴ� �Լ�
    /// </summary>
    public void DirectRotation()
    {
        if (!isRotating)
        {
            RotateTransform();
            ShowEffect();

            Invoke(nameof(ApplyChangedValue), rotationTime);

            isRotating = true;
        }
    }

    /// <summary>
    /// Ʈ�������� �������� ���ʷ� �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    public void InitTransformScale()
    {
        originScale = transform.localScale;
    }

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
        scaleSequence = DOTween.Sequence().SetAutoKill(false)
        .Append(transform.DOScale(new Vector3(transform.localScale.x / 1.5f, transform.localScale.y / 1.25f, transform.localScale.z / 1.5f), startScaleTime).SetEase(startScaleEase))
        .Append(transform.DOScale(originScale, endScaleTime).SetEase(endScaleEase));
    }

    /// <summary>
    /// ȸ������ ���� ��������Ʈ�� �����ϴ� �Լ�
    /// </summary>
    public void ChangeSprite()
    {
        transform.Find("Sprite").GetComponent<Animator>().SetFloat("Angle", transform.eulerAngles.y);
    }

    /// <summary>
    /// Transform�� ȸ����Ű�� �Լ�
    /// </summary>
    public void RotateTransform()
    {
        Vector3 targetRotation = transform.eulerAngles + new Vector3(0, 90, 0);
        transform.DORotate(targetRotation, rotationTime).SetEase(rotationEase);
    }

    /// <summary>
    /// ȸ���� ���� ���� ���� �����ϴ� �Լ�
    /// </summary>
    public void ApplyChangedValue()
    {
        isRotating = false;
    }
    #endregion
}
