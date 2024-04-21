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
        // ���� Y ��ġ�� ���� Y ��ġ�� ���̸� �̿��Ͽ� Y �ӵ��� ����մϴ�.
        float yVelocity = (transform.position.y - prevYPos) / Time.fixedDeltaTime;
        
        // ���� Y �ӵ��� �α׷� ����մϴ�.
        Debug.Log("Y �ӵ�: " + yVelocity);

        // ���� Y ��ġ�� ���� Y ��ġ�� ������Ʈ�մϴ�.
        prevYPos = transform.position.y;
    }
}
