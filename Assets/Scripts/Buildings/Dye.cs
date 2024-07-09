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

public class Dye : MonoBehaviour, IPooledObject
{
    [SerializeField] bool isBasicColor;

    public basicColorType basicColorType;
    public syntheticColorType syntheticColorType;
    public Color myColor;

    private basicColorType basicColorTypeTemp;
    private syntheticColorType syntheticColorTypeTemp;
    private Color myColorTemp;

    private Sprite[] syntheticSprites;
    private Color[] basicColors;
    private Color[] syntheticColors;

    [SerializeField] SpriteRenderer spriteRenderer;

    bool isMixed;

    private void SaveSettings()
    {
        basicColorTypeTemp = basicColorType;
        syntheticColorTypeTemp = syntheticColorType;
        myColorTemp = myColor;
    }

    private void Init()
    {
        basicColorType = basicColorTypeTemp;
        syntheticColorType = syntheticColorTypeTemp;
        myColor = myColorTemp;
    }

    private void Start()
    {
        SaveSettings();
        // �ʱ�ȭ�� �� ��ųʸ��� �⺻ ���� ���տ� ���� �׸� �߰�
        InitColorCombinations();
        ChangeToBasicColor();
    }

    // �⺻ ���� ���տ� ���� ��ųʸ� �ʱ�ȭ
    void InitColorCombinations()
    {
        // ��������Ʈ �� ���� �߰�
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

        // ���յ� ���� �̸� ����
        StringBuilder combinationKey = new StringBuilder();
        combinationKey.Append(firstColor.ToString());
        combinationKey.Append("_");
        combinationKey.Append(secondColor.ToString());

        // ��ųʸ����� �ش� ������ �ռ� ������ ã��
        if (DyeManager.instance.colorCombinationDict.TryGetValue(combinationKey.ToString(), out syntheticColorType resultColor))
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
        if (isBasicColor)
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
        else
        {
            switch (syntheticColorType)
            {
                case syntheticColorType.green:
                    myColor = syntheticColors[(int)syntheticColorType.green];
                    break;
                case syntheticColorType.purple:
                    myColor = syntheticColors[(int)syntheticColorType.purple];
                    break;
                case syntheticColorType.orange:
                    myColor = syntheticColors[(int)syntheticColorType.orange];
                    break;
            }
        }
    }

    public void OnObjectSpawn()
    {
        Init();
    }
}