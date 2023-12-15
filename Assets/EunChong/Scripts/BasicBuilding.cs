using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BasicBuilding : MonoBehaviour
{
    [SerializeField] float targetRotation;
    [SerializeField] float rotationSpeed;

    [SerializeField] bool isRotating;

    [SerializeField] Sprite[] sprites = new Sprite[4];

    /// <summary>
    /// ī�޶� �ٶ󺸴� �Լ�
    /// </summary>
    public void LookCameraRotation()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    /// <summary>
    /// ��ǥ ȸ������ �����ϴ� �Լ�
    /// </summary>
    public void SetTargetRotation()
    {
        if (!isRotating)
        {
            targetRotation += 90;
        }
    }

    /// <summary>
    /// ȸ������ ���� ��������Ʈ�� �����ϴ� �Լ�
    /// </summary>
    public void ChangeSprite()
    {
        switch(targetRotation)
        {
            case 0:
                transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = sprites[0];
                break;
            case 90:
                transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = sprites[1];
                break;
            case 180:
                transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = sprites[2];
                break;
            case 270:
                transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = sprites[3];
                break;
            case 360:
                transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = sprites[0];
                break;
        }
    }

    /// <summary>
    /// Y���� ȸ������ 360���� �Ѿ��� ���,
    /// <br></br>
    /// Y���� ȸ������ 0���� �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    public void ResetRotation()
    {
        if (Mathf.CeilToInt(transform.eulerAngles.y) >= 355)
        {
            targetRotation = 0;
        }
    }

    /// <summary>
    /// Transform�� ȸ����Ű�� �Լ�
    /// </summary>
    public void RotateTransform()
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
}
