using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Singleton<Tower>
{
    public List<TowerLevel> levels = new List<TowerLevel>();
    [SerializeField]
    private TowerLevel levelBasePrefab;
    [SerializeField]
    private List<TowerLevel> levelPrefabs;

    public List<GameObject> builderPoints;

    [SerializeField]
    SpriteRenderer buildProgressSR;
    [SerializeField]
    Sprite wall0, wall1, wall2;

    public GameObject pickupPos, placedownPos, storage, baseTower;
    public BezierCurve towerEntrance;

    public float LayerHeight= 4.31f;

    private int _progress;
    public int Progress { get => _progress; 
        set {
            _progress = value;
            if (_progress >= 20)
            {
                buildProgressSR.sprite = null;
                _progress = 0;
                NextLevel();
            }
            else if (_progress >= 15)
                buildProgressSR.sprite = wall2;
            else if (_progress >= 10)
                buildProgressSR.sprite = wall1;
            else if (_progress >= 5)
                buildProgressSR.sprite = wall0;
            else
                buildProgressSR.sprite = null;
        } 
    }

    public void NextLevel()
    {
        if(levels.Count == 0)
        {
            transform.Find("Base").gameObject.SetActive(false);
        }
        TowerLevel pref = levels.Count == 0 ? levelBasePrefab : levelPrefabs[Random.Range(0, levelPrefabs.Count)];
        levels.Add(Instantiate(pref, new Vector3(0,LayerHeight*levels.Count,0) + baseTower.transform.position,Quaternion.identity,transform));
        Vector3 up = new Vector3(0, LayerHeight);
        pickupPos.transform.position += up;
        placedownPos.transform.position += up;
        storage.transform.position += up;
        buildProgressSR.transform.position += up;
        for (int i = 0; i < builderPoints.Count; i++)
            builderPoints[i].transform.position += up;
        foreach (var builder in BuildStation.Instance.workers)
        {
            builder.transform.position += up;
            builder.ResetOrders();
        }
        foreach (var transporters in storage.GetComponent<MaterialStorage>().queuePlace)
            transporters.transform.position += up;
        levels[levels.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = levels.Count - 100;

        GameManager.Instance.NewLevelCashout();
    }

    private void Start()
    {
        Progress = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            NextLevel();
    }
}
