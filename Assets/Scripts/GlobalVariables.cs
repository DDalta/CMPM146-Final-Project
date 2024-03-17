using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalVariables : MonoBehaviour
{
    public GameObject totalTreasureText;
    public GameObject currentTreasureText;

    private static Text totalTreasureTextComp;
    private static Text currentTreasureTextComp;
    private static int _totalTreasure = 0;
    private static int _currentTreasure = 0;

    public static int TotalTreasure
    {
        get
        {
            return _totalTreasure;
        }
        set
        {
            _totalTreasure = value;
            totalTreasureTextComp.text = $"Total Treasure: {_totalTreasure}";

        }
    }

    public static int CurrentTreasure
    {
        get
        {
            return _currentTreasure;
        }
        set
        {
            _currentTreasure = value;
            currentTreasureTextComp.text = $"Collected: {_currentTreasure}";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        totalTreasureTextComp = totalTreasureText.GetComponent<Text>();
        currentTreasureTextComp = currentTreasureText.GetComponent<Text>();
    }

}
