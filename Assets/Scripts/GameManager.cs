using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public int Money { get; set; }

    public GameObject gameOverScreen;

    public PlanScript planScript;

    public Sprite speed1, speed2, speed4;
    public Image speedButton;

    public int yearInSeconds = 120;

    public float yearRemain;

    public int GameSpeed = 1;

    public int currentYear = 0;

    public int[] deadlines =
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
        if(currentYear == -1 ||  Tower.Instance.levels.Count < deadlines[currentYear] )
        {
            //LOSE TODO
            gameOverScreen.SetActive(true);
            currentYear = -1;
            return;
        }
        currentYear++;
        Money += PassiveIncomePerYear(currentYear);
        yearRemain = yearInSeconds;
        if (currentYear == deadlines.Length)
            SceneManager.LoadScene("EndScene");
        else
            planScript.Display();

    }

    private void Start()
    {
        Money = 50;
        StartNewYear();
    }

    private void Update()
    {
        yearRemain -= Time.deltaTime*GameManager.Instance.GameSpeed;
        if (yearRemain < 0)
            StartNewYear();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeSpeed()
    {
        switch (GameSpeed)
        {
            case 1:
                GameSpeed = 2;
                speedButton.sprite = speed2;
                break;
                
            case 2:
                GameSpeed = 4;
                speedButton.sprite = speed4;
                break;

            case 4:
                GameSpeed = 1;
                speedButton.sprite = speed1;
                break;
            default:
                break;
        }
    }
}
