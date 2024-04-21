using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum colorType
{
    red,
    orange,
    yellow,
    green,
    blue,
    magenta
}

public class Dye : MonoBehaviour
{
    public colorType colorType;
    public Color[] colors;
    public Color myColor;
 
    private void Start()
    {
        ChangeColor();
    }

    public void ChangeColor()
    {
        switch (colorType)
        {
            case colorType.red:
            {
                myColor = colors[(int)colorType.red];
                break;
            }
            case colorType.orange:
            {
                myColor = colors[(int)colorType.orange];
                break;
            }
            case colorType.yellow:
            {
                myColor = colors[(int)colorType.yellow];
                break;
            }
            case colorType.green:
            {
                myColor = colors[(int)colorType.green];
                break;
            }
            case colorType.blue:
            {
                myColor = colors[(int)colorType.blue];
                break;
            }
            case colorType.magenta:
            {
                myColor = colors[(int)colorType.magenta];
                break;
            }
        }
    }
}
