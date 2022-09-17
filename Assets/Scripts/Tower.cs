using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Singleton<Tower>
{
    public List<TowerLevel> levels = new List<TowerLevel>();
    [SerializeField]
    private TowerLevel levelPrefab;


    public GameObject pickupPos, placedownPos, storage;

    public float LayerHeight= 4.35f;

    public void NextLevel()
    {
        levels.Add(Instantiate(levelPrefab, new Vector3(0,LayerHeight*levels.Count,0) + transform.position,Quaternion.identity,transform));
        Vector3 up = new Vector3(0, LayerHeight);
        pickupPos.transform.position += up;
        placedownPos.transform.position += up;
        storage.transform.position += up;
        levels[levels.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = levels.Count - 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            NextLevel();
    }
}
