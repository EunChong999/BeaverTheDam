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

    // ��ųʸ��� ����Ͽ� �⺻ ���� ���տ� ���� �׸� ����
    Dictionary<string, syntheticColorType> colorCombinationDict = new Dictionary<string, syntheticColorType>();

    private void Start()
    {
        // �ʱ�ȭ�� �� ��ųʸ��� �⺻ ���� ���տ� ���� �׸� �߰�
        InitColorCombinations();
        ChangeToBasicColor();
    }

    // �⺻ ���� ���տ� ���� ��ųʸ� �ʱ�ȭ
    void InitColorCombinations()
    {
        // ������ ��� �⺻ ���� ���տ� ���� �׸� �߰�
        AddColorCombination(basicColorType.red, basicColorType.yellow, syntheticColorType.orange);
        AddColorCombination(basicColorType.yellow, basicColorType.red, syntheticColorType.orange);
        AddColorCombination(basicColorType.red, basicColorType.blue, syntheticColorType.purple);
        AddColorCombination(basicColorType.blue, basicColorType.red, syntheticColorType.purple);
        AddColorCombination(basicColorType.yellow, basicColorType.blue, syntheticColorType.green);
        AddColorCombination(basicColorType.blue, basicColorType.yellow, syntheticColorType.green);
    }

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

        // ���յ� ���� �̸� ����
        StringBuilder combinationKey = new StringBuilder();
        combinationKey.Append(firstColor.ToString());
        combinationKey.Append("_");
        combinationKey.Append(secondColor.ToString());

        // ��ųʸ����� �ش� ������ �ռ� ������ ã��
        if (colorCombinationDict.TryGetValue(combinationKey.ToString(), out syntheticColorType resultColor))
        {
            // �ռ� ���� ����
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
