using UnityEngine;

public class YVelocity : MonoBehaviour
{
    private float prevYPos;

    void Start()
    {
        prevYPos = transform.position.y;
    }

    void FixedUpdate()
    {
        // 현재 Y 위치와 이전 Y 위치의 차이를 이용하여 Y 속도를 계산합니다.
        float yVelocity = (transform.position.y - prevYPos) / Time.fixedDeltaTime;
        
        // 계산된 Y 속도를 로그로 출력합니다.
        Debug.Log("Y 속도: " + yVelocity);

        // 이전 Y 위치를 현재 Y 위치로 업데이트합니다.
        prevYPos = transform.position.y;
    }
}
