using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum basicColorType
{
    red,
    yellow,
    blue
}

public enum syntheticColorType
{
    orange,
    green,
    purple
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
 
    private void Start()
    {
        ChangeToBasicColor();
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

        switch ((int)firstColor + (int)secondColor)
        {
            case 1:
            {
                spriteRenderer.sprite = syntheticSprites[0];
                syntheticColorType = syntheticColorType.orange;
                myColor = syntheticColors[(int)syntheticColorType.orange];
                break;
            }
            case 2:
            {
                spriteRenderer.sprite = syntheticSprites[1];
                syntheticColorType = syntheticColorType.purple;
                myColor = syntheticColors[(int)syntheticColorType.purple];
                break;
            }
            case 3:
            {
                spriteRenderer.sprite = syntheticSprites[2];
                syntheticColorType = syntheticColorType.green;
                myColor = syntheticColors[(int)syntheticColorType.green];
                break;
            }
        }

        isMixed = false;
    }

    public void ChangeToBasicColor()
    {
        switch (basicColorType)
        {
            case basicColorType.red:
            {
                myColor = basicColors[(int)basicColorType.red];
                break;
            }
            case basicColorType.yellow:
            {
                myColor = basicColors[(int)basicColorType.yellow];
                break;
            }
            case basicColorType.blue:
            {
                myColor = basicColors[(int)basicColorType.blue];
                break;
            }
        }
    }
}
