using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPooledObject
{
    #region Variables
    public bool isCutted;
    public bool isPainted;
    public bool isMoving;
    public bool isCombined;
    public Shadow shadow;
    public Transform spriteTransform;
    public GameObject xSpriteObj;
    public GameObject zSpriteObj;
    public SpriteRenderer spriteRenderer;
    public Sprite replaceSprite;
    public Sprite[] cuttedSprites;
    public Sprite[] cuttedReplaceSprites;

    private Sprite spriteTemp;
    private Color spriteColorTemp;

    [HideInInspector] public Point curPoint;
    [HideInInspector] public bool canInput;

    [HideInInspector] public Color firstColor;
    [HideInInspector] public Color secondColor;

    [SerializeField] float startScaleTime;
    [SerializeField] float endScaleTime;
    [SerializeField] Ease rotationEase;
    [SerializeField] Ease startScaleEase;
    [SerializeField] Ease endScaleEase;

    Sequence itemScaleSequence;
    Vector3 originScale;
    #endregion
    #region Functions

    private void SaveSettings()
    {
        spriteTemp = spriteRenderer.sprite;
        spriteColorTemp = spriteRenderer.color;
    }

    private void Init()
    {
        spriteRenderer.sprite = spriteTemp;
        spriteRenderer.color = spriteColorTemp;
        isCutted = false;
        isMoving = false;
        isPainted = false;
        isCombined = false;
    }

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
    private void ChangeToCuttedSprites(bool isXType, bool isInput, bool isReversed)
    {
        if (isXType && isInput == true)
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

        if (isReversed)
        {
            return;
        }

        if (isXType && isInput == true)
        {
            spriteRenderer.sprite = cuttedSprites[2];
        }

        if (!isXType && isInput == true)
        {
            spriteRenderer.sprite = cuttedSprites[1];
        }

        if (isXType && isInput == false)
        {
            spriteRenderer.sprite = cuttedSprites[3];
        }

        if (!isXType && isInput == false)
        {
            spriteRenderer.sprite = cuttedSprites[0];
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
    public void CutSprite(bool isXType, bool cutterType, bool isReversed)
    {
        if (!isCutted)
        {
            xSpriteObj.SetActive(false);
            zSpriteObj.SetActive(false);

            ChangeToCuttedSprites(isXType, cutterType, isReversed);

            if (isPainted)
            {
                ChangeToCuttedReplaceSprites();
            }

            isCombined = false;
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
    public SpriteRenderer ApplyCutSprite(bool isXType, bool cutterType, bool isReversed)
    {
        ChangeToCuttedSprites(isXType, cutterType, isReversed);

        if (isPainted)
        {
            ChangeToCuttedReplaceSprites();
        }

        return spriteRenderer;
    }

    /// <summary>
    /// 잘려진 스프라이트를 결합하는 함수 
    /// </summary>
    public void CombineSprite(bool isXType, Color firstColor, Color secondColor)
    {
        spriteRenderer.sprite = null;
        shadow.isCutted = false;

        this.firstColor = firstColor;
        this.secondColor = secondColor;

        if (isXType)
        {
            xSpriteObj.SetActive(true);
            xSpriteObj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = firstColor;
            xSpriteObj.transform.GetChild(1).GetComponent<SpriteRenderer>().color = secondColor;
            zSpriteObj.SetActive(false);
        }
        else
        {
            xSpriteObj.SetActive(false);
            zSpriteObj.SetActive(true);
            zSpriteObj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = firstColor;
            zSpriteObj.transform.GetChild(1).GetComponent<SpriteRenderer>().color = secondColor;
        }

        isCombined = true;
        isCutted = false;
    }

    public void OnObjectSpawn()
    {
        Init();
    }
    #endregion
    #region Events

    private void Awake()
    {
        SaveSettings();
    }

    private void OnEnable()
    {
        originScale = spriteTransform.localScale;
    }

    private void OnDisable()
    {
        
    }
    #endregion
}
