using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Unity.VisualScripting;

public enum MapType
{
    time,
    count
}

public class MainManager : Manager
{
    public AudioSource test;
    public MapData curStage;
    [SerializeField]
    private StageDB stageDB;
    [SerializeField] TextMeshProUGUI limitTypeText;
    [SerializeField] GameObject clearUI;
    [SerializeField] GameObject failUI;
    [SerializeField] Transform endCard;
    [SerializeField] Transform[] star;
    [SerializeField] GameObject nextStage;
    [SerializeField] UICurveCount UICurve;
    public int clearScore;
    public int integratedCount;
    public int StageIndex;
    [HideInInspector] public bool isEnd;

    [Serializable]
    public struct MapData
    {
        public GameObject map;
        public string branch;
        public MapType type;
        public int entireLimitAmount;
        public int firstTimeLimit;
        public int secondTimeLimit;
        public int firstCountLimit;
        public int secondCountLimit;
        public int stars;
        public bool isCleared;
    }
    
    public MapData[] Maps;
    public static MainManager instance {get; private set;}
    private void Awake()
    {
        SetUp();
    }
    private void Start()
    {
        for (int i = 0; i < Maps.Length; i++)
        {
            Maps[i].isCleared = StringToBoolConverter.Convert(ReadExcelFile(ExcelFilePaths.StageFilePath, "Entites", i + 1, 6));
        }

        StageIndex = PlayerPrefs.GetInt("SelectIndex");
        endCard.localScale = Vector3.zero;
        nextStage.SetActive(StageIndex < PlayerPrefs.GetInt("MaxIndex"));
        for (int i = 0; i < star.Length; i++)
        {
            star[i].localScale = Vector3.zero;
        }

        curStage = Maps[StageIndex];
        integratedCount = curStage.entireLimitAmount;
        UICurve.SetCurveCount(integratedCount);

        Instantiate(curStage.map).SetActive(true);

        if (curStage.type == MapType.time)
        {
            StartCoroutine(MinusTime());
            limitTypeText.text = "TIME LIMIT";
        }
        
        if (curStage.type == MapType.count)
        {
            limitTypeText.text = "COUNT LIMIT";
        }

        UICurve.SetCurveCount(integratedCount);
    }

    public void SetUp()
    {
        instance = this;

        int index = 0;
        for (int i = 0; i < Maps.Length; i++)
        {
            if (stageDB.Entites[i].branch == Maps[index].branch)
            {
                Maps[index].type = (MapType)Enum.Parse(typeof(MapType), ReadExcelFile(ExcelFilePaths.StageFilePath, "Entites", i + 1, 1));
                Maps[index].entireLimitAmount = int.Parse(ReadExcelFile(ExcelFilePaths.StageFilePath, "Entites", i + 1, 2));
                Maps[index].firstTimeLimit = int.Parse(ReadExcelFile(ExcelFilePaths.StageFilePath, "Entites", i + 1, 3));
                Maps[index].firstCountLimit = int.Parse(ReadExcelFile(ExcelFilePaths.StageFilePath, "Entites", i + 1, 3));
                Maps[index].secondTimeLimit = int.Parse(ReadExcelFile(ExcelFilePaths.StageFilePath, "Entites", i + 1, 4));
                Maps[index].secondCountLimit = int.Parse(ReadExcelFile(ExcelFilePaths.StageFilePath, "Entites", i + 1, 4));
                Maps[index].stars = int.Parse(ReadExcelFile(ExcelFilePaths.StageFilePath, "Entites", i + 1, 5));
                index++;
            }
        }
    }

    private string ReadExcelFile(string filePath, string sheetName, int rowIndex, int colIndex)
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

    private void UpdateExcelFile(string filePath, string sheetName, int rowIndex, int colIndex, string value)
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

    public void AddRotateCount() => integratedCount++;
    public void MinusCurveCount()
    {
        integratedCount--;
        UICurve.SetCurveCount(integratedCount);
        if(integratedCount <= 0)
        {
            clearScore = 0;
            End(false);
        }
    }
    IEnumerator MinusTime()
    {
        while (true)
        {
            while (DialogSystem.instance != null && !DialogSystem.instance.isDialogSystemEnded)
                yield return null;

            yield return new WaitForSeconds(1);
            integratedCount--;
            UICurve.SetCurveCount(integratedCount);
            if (integratedCount <= 0)
            {
                clearScore = 0;
                End(false);
                yield break;
            }
        }
    }
    public void Cancel()
    {
        StartCoroutine(CancelAnim());
    }
    IEnumerator CancelAnim()
    {
        if (SceneAnim.instance.canAnim)
        {
            SceneAnim.instance.AnimOn();
            yield return new WaitForSeconds(0.5f);
            SceneLoad(Scenes.SelectScene);
        }
    }
    public void StartStage(bool isRetry)
    {
        StartCoroutine(InitStage(isRetry));
    }

    IEnumerator InitStage(bool isRetry)
    {
        if (SceneAnim.instance.canAnim)
        {
            SceneAnim.instance.AnimOn();
            yield return new WaitForSeconds(0.5f);
            if (!isRetry) PlayerPrefs.SetInt("SelectIndex", StageIndex + 1);
            SceneLoad(Scenes.MainScene);
        }
    }

    public void End(bool isCleared)
    {
        isEnd = true;

        StartCoroutine(EndMove());

        int clearIndex = 0;

        if ((isCleared || Maps[StageIndex].isCleared) && StageIndex < Maps.Length - 1)
        {
            clearIndex = StageIndex + 1;
            UpdateExcelFile(ExcelFilePaths.StageFilePath, "Entites", StageIndex + 1, 6, "TRUE");

            clearUI.SetActive(true);
            failUI.SetActive(false);
        }
        else
        {
            clearUI.SetActive(false);
            failUI.SetActive(true);
        }

        if (clearIndex > PlayerPrefs.GetInt("CanSelectIndex") && StageIndex < PlayerPrefs.GetInt("MaxIndex"))
            PlayerPrefs.SetInt("CanSelectIndex", clearIndex);
    }
    IEnumerator EndMove()
    {
        Time.timeScale = 1;

        yield return endCard.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).WaitForCompletion();

        for (int i = 0; i < clearScore; i++)
        {
            yield return star[i].DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).WaitForCompletion();
        }
    }
}
