using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int Money { get; set; }


    private int PassiceIncomePerYear(int year)
    {
        //TODO
        return 200;
    }

    private void Start()
    {
        Money = 260;
    }
}
