using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICurveCount : MonoBehaviour
{
    [SerializeField] Text countText;
    public void SetCurveCount(int setCount) => countText.text = setCount.ToString();
}
