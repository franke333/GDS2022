using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int Money { get; set; }

    private void Start()
    {
        Money = 260;
    }
}
