using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public float speed;

    //instance�� static���� �����ؼ� �ٸ� ������Ʈ������ ���� ����
    public static BuildingManager instance;

    void Awake()
    {
        if (instance != null) //�̹� �����ϸ�
        {
            /*Destroy(gameObject);*/ //�ΰ� �̻��̴� ����
            return;
        }
        instance = this; //�ڽ��� �ν��Ͻ���
        /*DontDestroyOnLoad(gameObject);*/ //�� �̵��ص� ��������ʰ�
    }
}
