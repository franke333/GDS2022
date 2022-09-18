using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanScript : MonoBehaviour
{
    public TMP_Text text;
    float displayRemain = 0;
    public void Display()
    {
        text.text = $"Built: {Tower.Instance.levels.Count}\n" +
            $"New Plan: {GameManager.Instance.deadlines[GameManager.Instance.currentYear] - Tower.Instance.levels.Count}";
        displayRemain = 7f;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        displayRemain -= Time.deltaTime;
        if (displayRemain < 0)
            gameObject.SetActive(false);
    }
}
