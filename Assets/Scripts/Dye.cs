using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public enum basicColorType
{
    red,
    yellow,
    blue
}

public enum syntheticColorType
{
    orange,
    purple,
    green
}

public class Dye : MonoBehaviour
{
    public basicColorType basicColorType;
    public syntheticColorType syntheticColorType;
    public Color myColor;
    private Sprite[] syntheticSprites;
    private Color[] basicColors;
    private Color[] syntheticColors;

    [SerializeField] SpriteRenderer spriteRenderer;

    bool isMixed;

    private void Start()
    {
        // 초기화할 때 딕셔너리에 기본 색상 조합에 대한 항목 추가
        InitColorCombinations();
        ChangeToBasicColor();
    }

    // 기본 색상 조합에 대한 딕셔너리 초기화
    void InitColorCombinations()
    {
        // 스프라이트 및 색상 추가
        syntheticSprites = DyeManager.instance.syntheticSprites;
        basicColors = DyeManager.instance.basicColors;
        syntheticColors = DyeManager.instance.syntheticColors;
    }

    public void MixColor(basicColorType firstColor, basicColorType secondColor)
    {
        if (isMixed)
        {
            return;
        }

        if (firstColor == secondColor)
        {
            return;
        }

        // 조합된 색상 이름 생성
        StringBuilder combinationKey = new StringBuilder();
        combinationKey.Append(firstColor.ToString());
        combinationKey.Append("_");
        combinationKey.Append(secondColor.ToString());

        // 딕셔너리에서 해당 조합의 합성 색상을 찾음
        if (DyeManager.instance.colorCombinationDict.TryGetValue(combinationKey.ToString(), out syntheticColorType resultColor))
        {
            // 합성 색상 설정
            spriteRenderer.sprite = syntheticSprites[(int)resultColor];
            syntheticColorType = resultColor;
            myColor = syntheticColors[(int)resultColor];
        }

        isMixed = false;
    }

    public void ChangeToBasicColor()
    {
        switch (basicColorType)
        {
            case basicColorType.red:
                myColor = basicColors[(int)basicColorType.red];
                break;
            case basicColorType.yellow:
                myColor = basicColors[(int)basicColorType.yellow];
                break;
            case basicColorType.blue:
                myColor = basicColors[(int)basicColorType.blue];
                break;
        }
    }
}
