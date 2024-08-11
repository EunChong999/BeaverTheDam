using UnityEngine;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

public static class ExcelFilePaths
{
#if UNITY_EDITOR
    public static readonly string DialogFilePath = Path.Combine(Application.dataPath, "Scripts", "Data", "DialogDB.xlsx");
    public static readonly string StageFilePath = Path.Combine(Application.dataPath, "Scripts", "Data", "StageDB.xlsx");
#else
    public static readonly string DialogFilePath = Path.Combine(Application.persistentDataPath, "DialogDB.xlsx");
    public static readonly string StageFilePath = Path.Combine(Application.persistentDataPath, "StageDB.xlsx");
#endif

    public static string ReadExcelFile(string filePath, string sheetName, int rowIndex, int colIndex)
    {
        // 파일이 존재하는지 확인
        if (!File.Exists(filePath))
        {
            Debug.LogError($"File not found: {filePath}");
            return null;
        }

        string cellValue = null;

        // 파일을 읽어들이기
        using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            IWorkbook workbook = new XSSFWorkbook(stream);
            ISheet sheet = workbook.GetSheet(sheetName);

            if (sheet != null)
            {
                IRow row = sheet.GetRow(rowIndex);
                if (row != null)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (cell != null)
                    {
                        cellValue = cell.ToString();
                    }
                }
            }
        }

        return cellValue;
    }

    public static void UpdateExcelFile(string filePath, string sheetName, int rowIndex, int colIndex, string value)
    {
        // 파일이 존재하는지 확인
        if (!File.Exists(filePath))
        {
            Debug.LogError($"File not found: {filePath}");
            return;
        }

        IWorkbook workbook;
        ISheet sheet;

        // 파일을 읽어들이기
        using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            workbook = new XSSFWorkbook(stream);
            sheet = workbook.GetSheet(sheetName) ?? workbook.CreateSheet(sheetName);
        }

        // 셀 값 업데이트
        IRow row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
        ICell cell = row.GetCell(colIndex) ?? row.CreateCell(colIndex);
        cell.SetCellValue(value);

        // 메모리 스트림을 사용하여 파일 쓰기
        using (MemoryStream memoryStream = new MemoryStream())
        {
            workbook.Write(memoryStream, true);
            File.WriteAllBytes(filePath, memoryStream.ToArray());
        }
    }
}
