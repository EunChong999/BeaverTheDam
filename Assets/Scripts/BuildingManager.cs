using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public float speed;

    //instance를 static으로 선언해서 다른 오브젝트에서도 접근 가능
    public static BuildingManager instance;

    void Awake()
    {
        if (instance != null) //이미 존재하면
        {
            /*Destroy(gameObject);*/ //두개 이상이니 삭제
            return;
        }
        instance = this; //자신을 인스턴스로
        /*DontDestroyOnLoad(gameObject);*/ //씬 이동해도 사라지지않게
    }
}
