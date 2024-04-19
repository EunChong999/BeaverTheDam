using System.Collections;

public interface IInputableBuilding
{
    /// <summary>
    /// 아이템을 들이는 함수
    /// </summary>
    void Input();

    /// <summary>
    /// 들인 아이템의 처리를 위해 대기시키는 함수
    /// </summary>
    IEnumerator WaitForInput();
}
