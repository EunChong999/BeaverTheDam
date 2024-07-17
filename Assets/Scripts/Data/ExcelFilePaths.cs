using UnityEngine;
using System.IO;

public static class ExcelFilePaths
{
#if UNITY_EDITOR
    public static readonly string DialogFilePath = Path.Combine(Application.dataPath, "Scripts", "Data", "DialogDB.xlsx");
    public static readonly string StageFilePath = Path.Combine(Application.dataPath, "Scripts", "Data", "StageDB.xlsx");
#else
    public static readonly string DialogFilePath = Path.Combine(Application.persistentDataPath, "DialogDB.xlsx");
    public static readonly string StageFilePath = Path.Combine(Application.persistentDataPath, "StageDB.xlsx");
#endif
}
