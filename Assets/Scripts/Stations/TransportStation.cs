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
    private MaterialStorage quarryStorage;

    private List<Worker> workers = new List<Worker>();

    private void GoToQuarry(Worker worker)
    {
        worker.OrderWalk(quarryPickUp.transform.position);
    }

    private void GoToTower(Worker worker)
    {
        var level = Tower.Instance.levels[0];
        worker.OrderWalk(level.front.be_start.transform.position);
    }
    
    private void UpTheTowerFront(Worker worker)
    {
        var level = Tower.Instance.levels[0];
        worker.OrderWalkBezier(level.front, level.traverseInS, 10);
    }

    private void UpTheTowerBack(Worker worker)
    {
        var level = Tower.Instance.levels[0];
        worker.OrderWalk(level.end.transform.position, false);
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
                //TODO
                GoToQuarry,
                quarryStorage.TryPickupMaterial,
                GoToTower,
                UpTheTowerFront,
                UpTheTowerBack
            };
        }
    }

    public override int CostNext()
    {
        throw new System.NotImplementedException();
    }

    public override int GetWorkersCount()
    {
        throw new System.NotImplementedException();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            AddWorkers(1);
    }
}
