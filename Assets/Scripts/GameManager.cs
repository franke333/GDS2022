using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int Money { get; set; }

    public int yearInSeconds = 120;

    private float yearRemain;

    public int currentYear = 0;

    private int[] deadlines =
        new int[] {0, 1, 2, 3, 5, 7, 9, 11, 14, 17, 20 };

    private int PassiveIncomePerYear(int year)
    {
        //TODO
        return 100*year+50;
    }

    private int IncomePerTowerLevel(int level)
    {
        return 100 + 25 * level;
    }

    public void NewLevelCashout()
    {
        Money += IncomePerTowerLevel(Tower.Instance.levels.Count - 1);
    }

    private void StartNewYear()
    {
        if(Tower.Instance.levels.Count < deadlines[currentYear])
        {
            //LOSE TODO
            Debug.Log("YOU LOST");
            return;
        }
        currentYear++;
        Money += PassiveIncomePerYear(currentYear);
        yearRemain = yearInSeconds;

    }

    private void Start()
    {
        Money = 50;
        StartNewYear();
    }

    private void Update()
    {
        yearRemain -= Time.deltaTime;
        if (yearRemain < 0)
            StartNewYear();
    }
}
