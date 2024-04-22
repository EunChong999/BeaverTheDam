using DG.Tweening;
using UnityEngine;

public class Item : MonoBehaviour
{
    #region Variables
    public bool isCutted;
    public bool isPainted;
    public bool isMoving;
    public Shadow shadow;
    public Transform spriteTransform;
    public SpriteRenderer spriteRenderer;
    public Sprite replaceSprite;
    public Sprite[] cuttedSprites;
    public Sprite[] cuttedReplaceSprites;

    [HideInInspector] public Point curPoint;
    [HideInInspector] public bool isZCombined;
    [HideInInspector] public bool isXCombined;
    [HideInInspector] public bool isZCutted;
    [HideInInspector] public bool isXCutted;

    [SerializeField] float startScaleTime;
    [SerializeField] float endScaleTime;
    [SerializeField] Ease rotationEase;
    [SerializeField] Ease startScaleEase;
    [SerializeField] Ease endScaleEase;

    Sequence itemScaleSequence;
    Vector3 originScale;
    #endregion
    #region Functions
    /// <summary>
    /// 이동 시작
    /// </summary>
    public void EnMove()
    {
        isMoving = true;
    }

    /// <summary>
    /// 이동 해체
    /// </summary>
    public void UnMove()
    {
        isMoving = false;
    }

    /// <summary>
    /// 회전시 트위닝 효과를 주는 함수
    /// </summary>
    public void ShowEffect(bool isSending)
    {
        if (isSending)
        {
            itemScaleSequence = DOTween.Sequence().SetAutoKill(true)
            .Append(spriteTransform.DOScale(new Vector3(spriteTransform.localScale.x / 1.5f, spriteTransform.localScale.y / 1.25f, spriteTransform.localScale.z / 1.5f), startScaleTime).SetEase(startScaleEase))
            .Append(spriteTransform.DOScale(originScale, endScaleTime).SetEase(endScaleEase));
        }
        else
        {
            itemScaleSequence = DOTween.Sequence().SetAutoKill(true)
            .Append(spriteTransform.DOScale(new Vector3(spriteTransform.localScale.x / 1.5f, spriteTransform.localScale.y / 1.25f, spriteTransform.localScale.z / 1.5f), startScaleTime).SetEase(startScaleEase))
            .Append(spriteTransform.DOScale(originScale, endScaleTime * 0.75f).SetEase(endScaleEase));
        }
    }

    /// <summary>
    /// 잘려진 스프라이트로 변경해주는 함수
    /// </summary>
    private void ChangeToCuttedSprites(bool isXType, bool isInput)
    {
        if (isXType && isInput  == true)
        {
            spriteRenderer.sprite = cuttedSprites[3];
        }

        if (!isXType && isInput == true)
        {
            spriteRenderer.sprite = cuttedSprites[0];
        }

        if (isXType && isInput == false)
        {
            spriteRenderer.sprite = cuttedSprites[2];
        }

        if (!isXType && isInput == false)
        {
            spriteRenderer.sprite = cuttedSprites[1];
        }
    }

    /// <summary>
    /// 잘려진 대체 스프라이트로 변경해주는 함수
    /// </summary>
    private void ChangeToCuttedReplaceSprites()
    {
        for (int i = 0; i < 4; i++)
        {
            if (spriteRenderer.sprite == cuttedSprites[i])
            {
                spriteRenderer.sprite = cuttedReplaceSprites[i];
                break;
            }
        }
    }

    /// <summary>
    /// 스프라이트를 자르는 함수
    /// </summary>
    public void CutSprite(bool isXType, bool cutterType)
    {
        if (!isCutted)
        {
            ChangeToCuttedSprites(isXType, cutterType);

            if (isPainted)
            {
                ChangeToCuttedReplaceSprites();
            }

            isCutted = true;
        }
    }

    /// <summary>
    /// 스프라이트를 칠하는 함수
    /// </summary>
    public void PaintSprite(Color color)
    {
        spriteRenderer.color = color;

        if (isCutted)
        {
            ChangeToCuttedReplaceSprites();
        }
        else
        {
            spriteRenderer.sprite = replaceSprite;
        }

        isPainted = true;
    }

    /// <summary>
    /// 잘려진 스프라이트를 반환하는 함수
    /// </summary>
    public SpriteRenderer ApplyCutSprite(bool isXType, bool cutterType)
    {
        ChangeToCuttedSprites(isXType, cutterType);

        if (isPainted)
        {
            ChangeToCuttedReplaceSprites();
        }

        return spriteRenderer;
    }
    #endregion
    #region Events
    private void OnEnable()
    {
        originScale = spriteTransform.localScale;
    }
    #endregion
}
