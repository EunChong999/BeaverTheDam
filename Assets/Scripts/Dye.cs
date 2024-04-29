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
    public Sprite[] syntheticSprites;
    public Color[] basicColors;
    public Color[] syntheticColors;
    public Color myColor;

    [SerializeField] SpriteRenderer spriteRenderer;

    bool isMixed;

    // 딕셔너리를 사용하여 기본 색상 조합에 대한 항목 저장
    Dictionary<string, syntheticColorType> colorCombinationDict = new Dictionary<string, syntheticColorType>();

    private void Start()
    {
        // 초기화할 때 딕셔너리에 기본 색상 조합에 대한 항목 추가
        InitColorCombinations();
        ChangeToBasicColor();
    }

    // 기본 색상 조합에 대한 딕셔너리 초기화
    void InitColorCombinations()
    {
        // 가능한 모든 기본 색상 조합에 대한 항목 추가
        AddColorCombination(basicColorType.red, basicColorType.yellow, syntheticColorType.orange);
        AddColorCombination(basicColorType.yellow, basicColorType.red, syntheticColorType.orange);
        AddColorCombination(basicColorType.red, basicColorType.blue, syntheticColorType.purple);
        AddColorCombination(basicColorType.blue, basicColorType.red, syntheticColorType.purple);
        AddColorCombination(basicColorType.yellow, basicColorType.blue, syntheticColorType.green);
        AddColorCombination(basicColorType.blue, basicColorType.yellow, syntheticColorType.green);
    }

    // 딕셔너리에 색상 조합 추가
    void AddColorCombination(basicColorType firstColor, basicColorType secondColor, syntheticColorType resultColor)
    {
        // 조합된 색상 이름 생성
        StringBuilder combinationKey = new StringBuilder();
        combinationKey.Append(firstColor.ToString());
        combinationKey.Append("_");
        combinationKey.Append(secondColor.ToString());

        // 딕셔너리에 추가
        colorCombinationDict.Add(combinationKey.ToString(), resultColor);
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
        if (colorCombinationDict.TryGetValue(combinationKey.ToString(), out syntheticColorType resultColor))
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
