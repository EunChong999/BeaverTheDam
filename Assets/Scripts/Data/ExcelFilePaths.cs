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
        // ������ �����ϴ��� Ȯ��
        if (!File.Exists(filePath))
        {
            Debug.LogError($"File not found: {filePath}");
            return null;
        }

        string cellValue = null;

        // ������ �о���̱�
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
        // ������ �����ϴ��� Ȯ��
        if (!File.Exists(filePath))
        {
            Debug.LogError($"File not found: {filePath}");
            return;
        }

        IWorkbook workbook;
        ISheet sheet;

        // ������ �о���̱�
        using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            workbook = new XSSFWorkbook(stream);
            sheet = workbook.GetSheet(sheetName) ?? workbook.CreateSheet(sheetName);
        }

        // �� �� ������Ʈ
        IRow row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
        ICell cell = row.GetCell(colIndex) ?? row.CreateCell(colIndex);
        cell.SetCellValue(value);

        // �޸� ��Ʈ���� ����Ͽ� ���� ����
        using (MemoryStream memoryStream = new MemoryStream())
        {
            workbook.Write(memoryStream, true);
            File.WriteAllBytes(filePath, memoryStream.ToArray());
        }
    }
}
