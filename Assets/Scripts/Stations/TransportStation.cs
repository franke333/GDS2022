using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportStation : AStation<TransportStation>
{
    [SerializeField]
    private Worker workerPrefab;
    [SerializeField]
    private GameObject spawnPoint, quarryPickUp;
    [SerializeField]
    private MaterialStorage quarryStorage,towerStorage;

    private void GoToQuarry(Worker worker)
    {
        worker.OrderWalk(quarryPickUp.transform.position);
    }

    private void GoToTowerStorage(Worker worker)
    {
        worker.OrderWalk(Tower.Instance.placedownPos.transform.position);
    }

    private void GoToTower(Worker worker)
    {
        var level = Tower.Instance.levels[0];
        worker.OrderWalk(level.front.be_start.transform.position);
    }
    
    private void ReturnToRailing(Worker worker)
    {
        var level = Tower.Instance.levels[worker.currentTowerLevel];
        worker.OrderWalk(level.end.transform.position, true);
    }
    private void UpTheTowerFront(Worker worker)
    {
        var level = Tower.Instance.levels[worker.currentTowerLevel];
        worker.OrderWalkBezier(level.front, level.traverseInS);
    }

    private void UpTheTowerBack(Worker worker)
    {
        var level = Tower.Instance.levels[worker.currentTowerLevel];
        worker.OrderWalk(level.end.transform.position, false);
    }

    private void UpTheTowerCheckLevel(Worker worker)
    {
        worker.OrderNextLevelIfExist(true);
    }

    private void DownTheTowerFront(Worker worker)
    {
        var level = Tower.Instance.levels[worker.currentTowerLevel];
        worker.OrderWalkBezier(level.front, level.traverseInS, true);
    }

    private void DownTheTowerBack(Worker worker)
    {
        var level = Tower.Instance.levels[worker.currentTowerLevel];
        worker.OrderWalk(level.front.be_end.transform.position, false);
    }

    private void DownTheTowerCheckLevel(Worker worker)
    {
        worker.OrderNextLevelIfExist(false);
    }

    public override string Name => "Transporters";

    public override void AddWorkers(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Worker worker = Instantiate(workerPrefab, spawnPoint.transform.position, Quaternion.identity);
            workers.Add(worker);
            worker.Name = $"Worker {workers.Count}";
            worker.orders = new List<dWorkerOrders>()
            {
                GoToQuarry,
                quarryStorage.TryPickupMaterial,
                GoToTower,
                UpTheTowerFront,
                UpTheTowerBack,
                UpTheTowerCheckLevel,
                GoToTowerStorage,
                towerStorage.TryPlaceMaterial,
                ReturnToRailing,
                DownTheTowerBack,
                DownTheTowerFront,
                DownTheTowerCheckLevel
            };
            worker.SetLayer(workers.Count*2);
        }
    }

    public override int CostNext()
    {
        throw new System.NotImplementedException();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            AddWorkers(1);
    }
}
