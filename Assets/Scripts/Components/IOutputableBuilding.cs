using System.Collections;

public interface IOutputableBuilding
{
    void Output();
    IEnumerator WaitOutputMove();
}
