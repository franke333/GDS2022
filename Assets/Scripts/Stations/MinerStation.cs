using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerStation : AStation<MinerStation>
{
    [SerializeField]
    private Worker workerPrefab;
    [SerializeField]
    private GameObject spawnPoint,targetWorkerPos;
    [SerializeField]
    private MaterialStorage quarryStorage;

    private List<Worker> workers = new List<Worker>();

    public override string Name => "Stone Quarry";

    private void SendWorkerOrder(Worker worker)
    {
        worker.OrderWalk(targetWorkerPos.transform.position);
    }

    private void ReturnWorkerOrder(Worker worker)
    {
        worker.OrderWalk(transform.position);
    }

    private void WaitInMineOrder(Worker worker)
    {
        worker.OrderWait(Random.Range(9, 12), false);
    }

    // todo disperse multiple workers at one point
    public override void AddWorkers(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Worker worker = Instantiate(workerPrefab, spawnPoint.transform.position, Quaternion.identity);
            workers.Add(worker);
            worker.Name = $"Worker {workers.Count}";
            worker.targetDest = targetWorkerPos.transform.position;
            worker.orders = new List<dWorkerOrders>()
            {
                SendWorkerOrder,
                quarryStorage.TryPlaceMaterial,
                ReturnWorkerOrder,
                WaitInMineOrder
            };
        }
    }

    public override int CostNext()
    {
        throw new System.NotImplementedException();
    }

    public override int GetWorkersCount()
    {
        return workers.Count;
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            AddWorkers(1);
    }
}
