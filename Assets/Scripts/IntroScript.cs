using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroScript : MonoBehaviour
{
    public List<string> texts;
    int index = 0;
    public TMP_Text textBubble;

    private void Start()
    {
        textBubble.text = texts[index];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            index++;
            if (index >= texts.Count)
                SceneManager.LoadScene("SampleScene");
            else
                textBubble.text = texts[index];
        }
    }
}
