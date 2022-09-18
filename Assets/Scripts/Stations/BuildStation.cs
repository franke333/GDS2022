using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildStation : AStation<BuildStation>
{
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private Worker workerPrefab;
    [SerializeField]
    private MaterialStorage towerStorage;
    public override string Name => "Build Station";

    private void MakeProgress(Worker worker)
    {
        Tower.Instance.Progress++;
        worker.NextOrder();
    }

    private void GoToStorage(Worker worker)
    {
        worker.OrderWalk(Tower.Instance.pickupPos.transform.position);
    }

    private void GoToWorkPlace(Worker worker)
    {
        var places = Tower.Instance.builderPoints;
        worker.OrderWalk(places[Random.Range(0, places.Count)].transform.position);
    }

    private void WorkHere(Worker worker)
    {
        worker.OrderWait(Random.Range(8f, 10f), true);
        worker.hasRock = false;
    }

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
                GoToStorage,
                towerStorage.TryPickupMaterial,
                GoToWorkPlace,
                WorkHere,
                MakeProgress
            };
            worker.SetLayer(workers.Count * 2);
        }
    }

    public override int CostNext()
    {
        return 10 + 5 * GetWorkersCount();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            AddWorkers(1);
    }
}
