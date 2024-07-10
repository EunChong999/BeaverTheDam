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

    // ��ųʸ��� ����Ͽ� �⺻ ���� ���տ� ���� �׸� ����
    public Dictionary<string, syntheticColorType> colorCombinationDict = new Dictionary<string, syntheticColorType>();

    // ��ųʸ��� ���� ���� �߰�
    void AddColorCombination(basicColorType firstColor, basicColorType secondColor, syntheticColorType resultColor)
    {
        // ���յ� ���� �̸� ����
        StringBuilder combinationKey = new StringBuilder();
        combinationKey.Append(firstColor.ToString());
        combinationKey.Append("_");
        combinationKey.Append(secondColor.ToString());

        // ��ųʸ��� �߰�
        colorCombinationDict.Add(combinationKey.ToString(), resultColor);
    }

    public static DyeManager instance;

    void Awake()
    {
        // ������ ��� �⺻ ���� ���տ� ���� �׸� �߰�
        AddColorCombination(basicColorType.red, basicColorType.yellow, syntheticColorType.orange);
        AddColorCombination(basicColorType.yellow, basicColorType.red, syntheticColorType.orange);
        AddColorCombination(basicColorType.red, basicColorType.blue, syntheticColorType.purple);
        AddColorCombination(basicColorType.blue, basicColorType.red, syntheticColorType.purple);
        AddColorCombination(basicColorType.yellow, basicColorType.blue, syntheticColorType.green);
        AddColorCombination(basicColorType.blue, basicColorType.yellow, syntheticColorType.green);

        instance = this;
    }
}
