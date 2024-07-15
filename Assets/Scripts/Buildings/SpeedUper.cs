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
        switch(BuildingManager.instance.speed)
        {
            case 2:
                myImage.sprite = sprites[1];
                BuildingManager.instance.speed = 4;
                break;
            case 4:
                myImage.sprite = sprites[2];
                BuildingManager.instance.speed = 6;
                break;
            case 6:
                myImage.sprite = sprites[0];
                BuildingManager.instance.speed = 2;
                break;
        }
    }
}
