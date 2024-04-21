using System.Collections;

public interface IOutputableBuilding
{
    /// <summary>
    /// 아이템을 내보내는 함수
    /// </summary>
    void Output();

    /// <summary>
    /// 내보낸 아이템의 처리를 위해 대기시키는 함수
    /// </summary>
    IEnumerator WaitForOutput();
}
