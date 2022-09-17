using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AStation<T> : Singleton<AStation<T>>
{

    public abstract string Name { get; }
    public abstract int GetWorkersCount();

    public abstract void AddWorkers(int count);

    public abstract int CostNext();

    public virtual void Buy()
    {
        int cost = CostNext();
        if(cost > GameManager.Instance.Money)
        {
            Debug.LogError($"Not enough money to buy worker at {Name}");
            return;
        }
        GameManager.Instance.Money -= cost;
        AddWorkers(1);
    }
}
