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
    /// �̵� ����
    /// </summary>
    public void EnMove()
    {
        isMoving = true;
    }

    /// <summary>
    /// �̵� ��ü
    /// </summary>
    public void UnMove()
    {
        isMoving = false;
    }

    /// <summary>
    /// ȸ���� Ʈ���� ȿ���� �ִ� �Լ�
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
    /// �߷��� ��������Ʈ�� �������ִ� �Լ�
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
    /// �߷��� ��ü ��������Ʈ�� �������ִ� �Լ�
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
    /// ��������Ʈ�� �ڸ��� �Լ�
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
    /// ��������Ʈ�� ĥ�ϴ� �Լ�
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
    /// �߷��� ��������Ʈ�� ��ȯ�ϴ� �Լ�
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
