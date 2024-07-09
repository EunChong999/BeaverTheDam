using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum allColorType
{
    red,
    yellow,
    blue,
    orange,
    purple,
    green
}

public class DyeManager : MonoBehaviour
{
    public Sprite[] syntheticSprites;
    public Color[] basicColors;
    public Color[] syntheticColors;

    // 딕셔너리를 사용하여 기본 색상 조합에 대한 항목 저장
    public Dictionary<string, syntheticColorType> colorCombinationDict = new Dictionary<string, syntheticColorType>();

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

    public static DyeManager instance;

    void Awake()
    {
        // 가능한 모든 기본 색상 조합에 대한 항목 추가
        AddColorCombination(basicColorType.red, basicColorType.yellow, syntheticColorType.orange);
        AddColorCombination(basicColorType.yellow, basicColorType.red, syntheticColorType.orange);
        AddColorCombination(basicColorType.red, basicColorType.blue, syntheticColorType.purple);
        AddColorCombination(basicColorType.blue, basicColorType.red, syntheticColorType.purple);
        AddColorCombination(basicColorType.yellow, basicColorType.blue, syntheticColorType.green);
        AddColorCombination(basicColorType.blue, basicColorType.yellow, syntheticColorType.green);

        instance = this;
    }
}
