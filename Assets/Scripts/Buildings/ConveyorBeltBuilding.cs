using UnityEngine;

public class ConveyorBeltBuilding : BasicBuilding
{
    #region Variables

    [Header("ConveyorBeltBuilding")]

    [Space(10)]

    private bool canPlay;
    private int rotationAngle;
    #endregion
    #region Functions
    /// <summary>
    /// 트레일러의 방향을 바꾸는 함수
    /// </summary>
    private void ChangeTrailerDirection()
    {
        if (canPlay)
        {
            rotationAngle = Mathf.RoundToInt(transform.eulerAngles.y);

            if (movementType == movementType.straightType)
            {
                if ((rotationAngle >= 0 && rotationAngle < 90) ||
                    (rotationAngle >= 90 && rotationAngle < 180))
                {
                    spriteAnimator.SetFloat("Speed", 1);
                }

                if ((rotationAngle >= 180 && rotationAngle < 270) ||
                    (rotationAngle >= 270 && rotationAngle < 360))
                {
                    spriteAnimator.SetFloat("Speed", -1);
                }
            }

            if (movementType == movementType.curveType)
            {
                if (directionType == directionType.rightType)
                {
                    if (rotationAngle >= 0 && rotationAngle < 90 ||
                       (rotationAngle >= 90 && rotationAngle < 180 ||
                       (rotationAngle >= 180 && rotationAngle < 270)))
                    {
                        spriteAnimator.SetFloat("Speed", 1);
                    }

                    if (rotationAngle >= 270 && rotationAngle < 360)
                    {
                        spriteAnimator.SetFloat("Speed", -1);
                    }
                }
                else
                {
                    if (rotationAngle >= 0 && rotationAngle < 90 ||
                       (rotationAngle >= 90 && rotationAngle < 180 ||
                       (rotationAngle >= 180 && rotationAngle < 270)))
                    {
                        spriteAnimator.SetFloat("Speed", -1);
                    }

                    if (rotationAngle >= 270 && rotationAngle < 360)
                    {
                        spriteAnimator.SetFloat("Speed", 1);
                    }
                }
            }
        }
        else
        {
            spriteAnimator.SetFloat("Speed", 0);
        }
    }

    /// <summary>
    /// 트레일러의 타입을 선택하는 함수
    /// </summary>
    private void SetTrailerType()
    {
        switch (movementType)
        {
            case movementType.straightType:
                spriteAnimator.SetBool("IsStraight", true);
                break;
            case movementType.curveType:
                spriteAnimator.SetBool("IsStraight", false);
                break;
        }
    }

    /// <summary>
    /// 포인트를 확인하는 함수
    /// </summary>
    private void CheckPoint()
    {
        canPlay = point.canPlay;
    }

    /// <summary>
    /// 회전각에 따라 스프라이트를 변경하는 함수
    /// </summary>
    private void ChangeSprite()
    {
        spriteAnimator.SetInteger("Angle", Mathf.RoundToInt(transform.eulerAngles.y));
    }
    #endregion
    #region Events
    private void OnMouseOver()
    {
        // 마우스 좌클릭
        if (Input.GetMouseButtonDown(0) && !isRotating)
        {
            DirectRotation(false, targetAngle, transform);
        }

        // 마우스 우클릭
        else if (Input.GetMouseButtonDown(1) && !isRotating)
        {
            DirectRotation(true, targetAngle, transform);
        }

        // 마우스 휠클릭
        else if (Input.GetMouseButtonDown(2) && !isRotating)
        {
            ChangeDirectionType();

            if (movementType == movementType.curveType)
                detector.ExchangeDetector();
        }
    }

    private void Start()
    {
        InitSettings();
        SetTrailerType();
    }

    private void Update()
    {
        CheckPoint();
        ChangeSprite();
        ChangeTrailerDirection();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
