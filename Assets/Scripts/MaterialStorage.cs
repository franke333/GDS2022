using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialStorage : MonoBehaviour
{
    public int limit = 5;
    public int count = 0;

    Queue<Worker> queuePlace = new Queue<Worker>();
    Queue<Worker> queueTake = new Queue<Worker>();

    public void TryPlaceMaterial(Worker worker)
    {
        if (limit != count)
        {
            if (count == 0 && queueTake.Count != 0)
                queueTake.Dequeue().NextOrder();
            else
                count++;
            worker.NextOrder();
        }
        else
        {
            queuePlace.Enqueue(worker);
            worker.OrderWaitOnCall(true);
            //TODO place worker in queue area
            worker.transform.position += new Vector3(Random.Range(-0.65f, 0), Random.Range(-0.35f, 0.35f), 0);
        }
    }

    public void TryPickupMaterial(Worker worker)
    {
        if (count != 0)
        {
            count--;
            worker.NextOrder();
        }
        else if(count == limit && queuePlace.Count != 0)
        {
            worker.NextOrder();
            queuePlace.Dequeue().NextOrder();
        }
        else
        {
            queueTake.Enqueue(worker);
            worker.OrderWaitOnCall(true);
            //TODO place worker in queue area
            worker.transform.position += new Vector3(Random.Range(0, 0.65f), Random.Range(-0.35f, 0.35f), 0);
        }
    }
}
