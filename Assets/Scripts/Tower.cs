using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Singleton<Tower>
{
    public List<TowerLevel> levels = new List<TowerLevel>();
    [SerializeField]
    private TowerLevel levelPrefab;

    public List<GameObject> builderPoints;

    public GameObject pickupPos, placedownPos, storage;

    public float LayerHeight= 4.35f;

    public int progress = 0;

    public void NextLevel()
    {
        levels.Add(Instantiate(levelPrefab, new Vector3(0,LayerHeight*levels.Count,0) + transform.position,Quaternion.identity,transform));
        Vector3 up = new Vector3(0, LayerHeight);
        pickupPos.transform.position += up;
        placedownPos.transform.position += up;
        storage.transform.position += up;
        for (int i = 0; i < builderPoints.Count; i++)
            builderPoints[i].transform.position += up;
        foreach (var builder in BuildStation.Instance.workers)
            builder.transform.position += up;
        foreach (var transporters in storage.GetComponent<MaterialStorage>().queuePlace)
            transporters.transform.position += up;
        levels[levels.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = levels.Count - 100;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            NextLevel();
    }
}
