using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildStation : AStation<BuildStation>
{
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private Worker workerPrefab;
    public override string Name => "Build Station";

    private void MakeProgress(Worker worker)
    {

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
            };
            worker.SetLayer(workers.Count * 2);
        }
    }

    public override int CostNext()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            AddWorkers(1);
    }
}
