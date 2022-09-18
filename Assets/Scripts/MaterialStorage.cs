using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialStorage : MonoBehaviour
{
    public int limit = 5;
    public int count = 0;

    public Queue<Worker> queuePlace = new Queue<Worker>();
    public Queue<Worker> queueTake = new Queue<Worker>();

    [SerializeField]
    Sprite stash0, stash1, stash2;
    [SerializeField]
    SpriteRenderer sr;


    [SerializeField]
    GameObject updateSign;
    int currentLevel = 0;
    static int[] upgradeCosts
        = new int[] { 50, 100 };

    static int[] upgradeLimits
        = new int[] { 5, 25, 125 };


    public void TryPlaceMaterial(Worker worker)
    {
        if(queueTake.Count != 0)
        {
            Worker newworker = queueTake.Dequeue();
            newworker.hasRock = true;
            newworker.NextOrder();
            worker.hasRock = false;
            worker.NextOrder();
        }
        else if (limit != count)
        {
            count++;
            UpdateSprite();
            worker.hasRock = false;
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
        if(queuePlace.Count != 0)
        {
            worker.hasRock = true;
            worker.NextOrder();
            Worker newworker = queuePlace.Dequeue();
            newworker.hasRock = false;
            newworker.NextOrder();
        }
        else if (count != 0)
        {
            count--;
            UpdateSprite();
            worker.hasRock = true;
            worker.NextOrder();
        }
        else
        {
            queueTake.Enqueue(worker);
            worker.OrderWaitOnCall(true);
            //TODO place worker in queue area
            worker.transform.position += new Vector3(Random.Range(0, 0.65f), Random.Range(-0.35f, 0.35f), 0);
        }
    }

    void UpdateSprite()
    {
        if (count == 0)
            sr.sprite = stash0;
        else if (count == limit)
            sr.sprite = stash2;
        else
            sr.sprite = stash1;
    }

    private void Update()
    {
        updateSign.SetActive(!(currentLevel >= upgradeCosts.Length || GameManager.Instance.Money < upgradeCosts[currentLevel]));
    }

    public void BuyUpgrade()
    {
        if (currentLevel >= upgradeCosts.Length || GameManager.Instance.Money < upgradeCosts[currentLevel])
            return;
        GameManager.Instance.Money -= upgradeCosts[currentLevel++];
        limit = upgradeLimits[currentLevel];
        transform.localScale *= 1.25f;
    }
}
