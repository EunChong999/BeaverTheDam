using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BasicBuilding : MonoBehaviour
{
    [SerializeField] float rotationTime;
    [SerializeField] Ease moveEase;
    [SerializeField] Ease scaleEase;
    [SerializeField] Sprite[] sprites = new Sprite[4];

    bool isRotating;
    int rotationCount;

    /// <summary>
    /// 카메라를 바라보는 함수
    /// </summary>
    public void LookCameraRotation()
    {
        transform.Find("Sprite").LookAt(transform.Find("Sprite").position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    /// <summary>
    /// 회전시 트위닝 효과를 주는 함수
    /// </summary>
    public void ShowEffect()
    {

    }

    /// <summary>
    /// 회전각에 따라 스프라이트를 변경하는 함수
    /// </summary>
    public void ChangeSprite()
    {
        switch(rotationCount)
        {
            case 0:
                transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = sprites[0];
                break;
            case 1:
                transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = sprites[1];
                break;
            case 2:
                transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = sprites[2];
                break;
            case 3:
                transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = sprites[3];
                break;
        }
    }

    /// <summary>
    /// Transform을 회전시키는 함수
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
    /// 회전을 통해 변한 값을 적용하는 함수
    /// </summary>
    private void ApplyChangedValue()
    {
        rotationCount++;

        if (rotationCount > 3)
        {
            rotationCount = 0;
        }

        isRotating = false;
    }
}
