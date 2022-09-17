using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportStation : AStation<TransportStation>
{
    public override string Name => "Transporters";

    public override void AddWorkers(int count)
    {
        throw new System.NotImplementedException();
    }

    public override int CostNext()
    {
        throw new System.NotImplementedException();
    }

    public override int GetWorkersCount()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
