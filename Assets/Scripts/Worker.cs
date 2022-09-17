using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void dWorkerOrders(Worker worker);

public enum WorkerOrderType
{
    Walk,WalkBezier,WalkBezierReversed,WaitTime,WaitUntilCalled
}

public class Worker : MonoBehaviour
{
    public string Name;
    public string workerType;

    public List<dWorkerOrders> orders;

    [SerializeField]
    private float speed=1f;

    private SpriteRenderer srHat;

    private WorkerOrderType orderType;
    private float waitTime;
    private Vector2 targetDest;

    private int orderIndex = 0;
    public int currentTowerLevel = 0;

    private BezierCurve bc;
    private float walkOverSeconds;
    private float currentWalkTime;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        srHat = transform.GetChild(0).GetComponent<SpriteRenderer>();
        orderIndex=-1;
        NextOrder();
    }

    public void OrderWait(float duration,bool visible)
    {
        orderType = WorkerOrderType.WaitTime;
        waitTime = duration;
        EnableDraw(visible);
    }

    public void OrderWalk(Vector3 destination,bool visible=true)
    {
        EnableDraw(visible);
        orderType = WorkerOrderType.Walk;
        targetDest = destination;
    }

    public void OrderWaitOnCall(bool visible)
    {
        EnableDraw(visible);
        orderType = WorkerOrderType.WaitUntilCalled;
    }

    public void OrderWalkBezier(BezierCurve curve,float inSeconds,bool reversed = false)
    {
        EnableDraw(true);
        orderType = reversed ? WorkerOrderType.WalkBezierReversed : WorkerOrderType.WalkBezier;
        SetLayer(currentTowerLevel + 2);
        bc = curve;
        walkOverSeconds = inSeconds;
        currentWalkTime = reversed ? inSeconds : 0;
    }

    public void OrderNextLevelIfExist(bool up)
    {
        if (up)
        {
            if (currentTowerLevel != Tower.Instance.levels.Count - 1)
            {
                currentTowerLevel++;
                orderIndex -= 3;
            }
        }
        else
        {
            if (currentTowerLevel != 0)
            {
                orderIndex -= 3;
                currentTowerLevel--;
            }
        }
        NextOrder();
    }

    public void NextOrder()
    {
        orderIndex++;
        if (orderIndex == orders.Count)
            orderIndex = 0;
        orders[orderIndex].Invoke(this);
    }
    

    private void Update()
    {
        switch (orderType)
        {
            case WorkerOrderType.Walk:
                Vector3 target = new Vector3(targetDest.x, targetDest.y);
                Vector3 dir = (target - transform.position);
                if (Vector3.Magnitude(dir) <= speed * Time.deltaTime)
                {
                    transform.position = target;
                    NextOrder();
                }
                else
                {
                    transform.Translate(dir.normalized * speed * Time.deltaTime);
                }
                break;
            case WorkerOrderType.WaitTime:
                waitTime -= Time.deltaTime;
                if (waitTime < 0)
                    NextOrder();
                break;
            case WorkerOrderType.WalkBezier:
                currentWalkTime += Time.deltaTime;
                if (currentWalkTime >= walkOverSeconds)
                {
                    transform.position = bc.be_end.transform.position;
                    NextOrder();
                }
                else
                    transform.position = bc.PosAtTime(currentWalkTime / walkOverSeconds);
                break;
            case WorkerOrderType.WaitUntilCalled:
                break;
            case WorkerOrderType.WalkBezierReversed:
                currentWalkTime -= Time.deltaTime;
                if (currentWalkTime <= 0)
                {
                    transform.position = bc.be_start.transform.position;
                    NextOrder();
                }
                else
                    transform.position = bc.PosAtTime( currentWalkTime / walkOverSeconds);
                break;
            default:
                break;
        }

    }

    private void SetLayer(int layer)
    {
        sr.sortingOrder = layer;
        srHat.sortingOrder = layer + 1;
    }

    private void EnableDraw(bool value)
    {
        sr.enabled = value;
        srHat.enabled = value;
    }

}
