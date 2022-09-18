using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerLevel : MonoBehaviour
{
    public BezierCurve front;
    public GameObject end;

    public ElevatorScript elevator;

    public float traverseInS;


    [SerializeField]
    GameObject buyElevButton;

    private void Update()
    {
        if (elevator.elevatorLevel == ElevatorScript.upgradeCosts.Length - 1)
        {
            buyElevButton.SetActive(false);
            return;
        }
        buyElevButton.SetActive(GameManager.Instance.Money >= ElevatorScript.upgradeCosts[elevator.elevatorLevel + 1]);
    }
}
