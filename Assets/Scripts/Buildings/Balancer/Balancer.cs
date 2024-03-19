using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balancer : DoubleBasicBuilding
{
    // Transform curItem; //현재 들어간 아이템
    // int curCount; //균등분배를 위한 카운트(들어간 후 증감)
    // private void Update()
    // {
    //    for(int i = 0; i < buildings.Length; i++) //두 컨베이어 벨트만큼 반복
    //    {
    //        var build = buildings[i].point;
    //        if(build.itemTransform != null //컨베이어 벨트에 Item이 있고
    //        && build.itemTransform != curItem //전에 들어갔던 아이템이 아니면서(중복 이동 방지)
    //        && Vector3.Distance(build.itemTransform.position,build.transform.position) <= 0.01f) //벨트와의 거리가 가까우면
    //        {
    //            curItem = build.itemTransform; //현재 아이템을 전에 들어간 것으로 저장하고
    //            int curIndex = CalculateMoveConveyor(i); //현재 Output이 연결되어 있는 컨베이어 벨트의 Index를 저장
    //            if(curIndex == -1) return; //만약 없다면 함수 취소
    //            build.itemTransform.position 
    //            = buildings[curIndex].transform.position; //아이템의 위치를 저장한 Index의 컨베이어 벨트로 이동
    //            curCount++; //균등 분배를 위해 증감
    //        }
    //    }
    // }
    // public int CalculateMoveConveyor(int startIndex) //현재 Output이 연결되어 있는 컨베이어 벨트의 Index를 저장
    // {
    //    for(int i = 0; i < buildings.Length; i++)
    //    {
    //        if(buildings[(startIndex + i + curCount) % buildings.Length].canMove) //만약 curCount + Index의 벨트가 이동 가능하다면
    //        {
    //            return (startIndex + i + curCount) % buildings.Length; //이동 가능한 Index를 반환
    //        }
    //    }
    //    return -1; //없다면 오류 Index 반환
    // }
}
