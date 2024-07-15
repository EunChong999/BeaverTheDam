using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUper : MonoBehaviour
{
    [SerializeField]
    private Image myImage;
    [SerializeField]
    private Sprite[] sprites;

    public void OnSpeedUp()
    {
        switch(Time.timeScale)
        {
            case 1:
                myImage.sprite = sprites[1];
                Time.timeScale = 2;
                break;
            case 2:
                myImage.sprite = sprites[2];
                Time.timeScale = 3;
                break;
            case 3:
                myImage.sprite = sprites[0];
                Time.timeScale = 1;
                break;
        }
    }
}
