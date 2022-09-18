using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class StationInfoUI
{
    public TMP_Text counter;
    public Button buttonPrice;
    public TMP_Text price;
}

public class WorkerMarket : MonoBehaviour
{
    public StationInfoUI builderSt, minerSt, transportSt;

    private void RefreshUI()
    {
        int[] workersCount = new int[] {
            BuildStation.Instance.GetWorkersCount(),
            MinerStation.Instance.GetWorkersCount(),
            TransportStation.Instance.GetWorkersCount()
        };

        int[] prices = new int[] {
            BuildStation.Instance.CostNext(),
            MinerStation.Instance.CostNext(),
            TransportStation.Instance.CostNext()
        };

        StationInfoUI[] stations = new StationInfoUI[] { builderSt, minerSt, transportSt };

        for (int i = 0; i < 3; i++)
        {
            stations[i].counter.text = workersCount[i].ToString();
            stations[i].price.text = prices[i].ToString();
            stations[i].buttonPrice.interactable = GameManager.Instance.Money >= prices[i];
        }
    }

    public void BuyWorker(int index)
    {
        switch (index)
        {
            case 0:
                BuildStation.Instance.AddWorkers(1);
                break;
            case 1:
                MinerStation.Instance.AddWorkers(1);
                break;
            case 2:
                TransportStation.Instance.AddWorkers(1);
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        RefreshUI();
    }
}
