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

    private void GoToTowerBezier(Worker worker)
    {

        worker.OrderWalkBezier(Tower.Instance.towerEntrance,4f);
    }

    private void GoToQuarryBezier(Worker worker)
    {

        worker.OrderWalkBezier(Tower.Instance.towerEntrance, 4f, true);
    }
    private void GoToTowerBeforeBezier(Worker worker)
    {
        if (Tower.Instance.levels.Count == 0)
        {
            worker.JumpToOrder(worker.orders.IndexOf(GoToTowerStorage));
            return;
        }
        worker.OrderWalk(Tower.Instance.towerEntrance.be_start.transform.position);
    }

    private void ReturnToRailing(Worker worker)
    {
        if (Tower.Instance.levels.Count == 0)
        {
            worker.JumpToOrder(worker.orders.IndexOf(GoToQuarry));
            return;
        }
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

    public void StartGoingUp(Worker worker)
    {
        int index = worker.orders.IndexOf(UpTheTowerCheckLevel);
        worker.JumpToOrder(index);
    }
    public void StartGoingDown(Worker worker)
    {
        int index = worker.orders.IndexOf(DownTheTowerCheckLevel);
        worker.JumpToOrder(index);
    }

    private void TryToFillElevator(Worker worker)
    {
        ElevatorScript elevator = Tower.Instance.levels[worker.currentTowerLevel].elevator;
        if (elevator == null)
        {
            worker.NextOrder();
            return;
        }
        elevator.TryPlaceDown(worker);
    }

    private void TryToGrabFromElevator(Worker worker)
    {
        ElevatorScript elevator = Tower.Instance.levels[worker.currentTowerLevel].elevator;
        if (elevator == null)
        {
            worker.NextOrder();
            return;
        }
        elevator.TryPickUp(worker);
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
                GoToTowerBeforeBezier,
                GoToTowerBezier,
                UpTheTowerFront,
                UpTheTowerBack,
                TryToFillElevator,
                UpTheTowerCheckLevel,
                GoToTowerStorage,
                towerStorage.TryPlaceMaterial,
                ReturnToRailing,
                DownTheTowerBack,                               
                DownTheTowerFront,
                TryToGrabFromElevator,
                DownTheTowerCheckLevel,
                GoToQuarryBezier
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
