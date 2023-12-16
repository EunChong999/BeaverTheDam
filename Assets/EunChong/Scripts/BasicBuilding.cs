using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBuilding : MonoBehaviour
{
    public float rotationTime;

    [SerializeField] Ease rotationEase;
    [SerializeField] float startScaleTime;
    [SerializeField] float endScaleTime;
    [SerializeField] Ease startScaleEase;
    [SerializeField] Ease endScaleEase;

    [HideInInspector] public bool isRotating;
    [HideInInspector] public Vector3 originScale;

    Sequence scaleSequence;

    /// <summary>
    /// 회전에 대한 전체적인 동작을 지시하는 함수
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
    /// 트랜스폼의 스케일을 최초로 초기화하는 함수
    /// </summary>
    public void InitTransformScale()
    {
        originScale = transform.localScale;
    }

    /// <summary>
    /// 카메라를 바라보는 함수
    /// </summary>
    public void LookCameraRotation()
    {
        foreach (Transform t in transform)
        {
            t.LookAt(t.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }

    /// <summary>
    /// 회전시 트위닝 효과를 주는 함수
    /// </summary>
    public void ShowEffect()
    {
        scaleSequence = DOTween.Sequence().SetAutoKill(false)
        .Append(transform.DOScale(new Vector3(transform.localScale.x / 1.5f, transform.localScale.y / 1.25f, transform.localScale.z / 1.5f), startScaleTime).SetEase(startScaleEase))
        .Append(transform.DOScale(originScale, endScaleTime).SetEase(endScaleEase));
    }

    /// <summary>
    /// 회전각에 따라 스프라이트를 변경하는 함수
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
    /// Transform을 회전시키는 함수
    /// </summary>
    public void RotateTransform()
    {
        Vector3 targetRotation = transform.eulerAngles + new Vector3(0, 90, 0);
        transform.DORotate(targetRotation, rotationTime).SetEase(rotationEase);
    }

    /// <summary>
    /// 회전을 통해 변한 값을 적용하는 함수
    /// </summary>
    public void ApplyChangedValue()
    {
        isRotating = false;
        transform.localScale = originScale;
    }
}
