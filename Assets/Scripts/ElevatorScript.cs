using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    enum ElevatorState { NotFull, FullWaiting, TravelUp, TravelDown }
    public int stashLimit, stashCurrent, elevatorLimit, elevatorCurrent;

    [SerializeField]
    GameObject yElevMin, yElevMax, elevGO;
    [SerializeField]
    float speed = 1f;
    [SerializeField]
    SpriteRenderer stash,bucket;

    [SerializeField]
    Sprite stash0, stash1, stash2;
    [SerializeField]
    List<Sprite> bucketsEmpty, bucketsFull;

    [SerializeField]
    SpriteMask sm;
    [SerializeField]
    TowerLevel towerLevel;

    TransportStation ts;
    ElevatorState state;

    private void Start()
    {
        ts = TransportStation.Instance as TransportStation;
    }
    public static int[] upgradeCosts
        = new int[] { 50,100, 200 }; //TODO
    public static int initCost => upgradeCosts[0];
    private static int[] upgradesElevators
        = new int[] { 1, 5, 10 };
    private static int[] upgradeStashes
        = new int[] { 4, 15, 20 };
    public int elevatorLevel = -1;
    public void BuyUpgradeElevator()
    {
        if (elevatorLevel == upgradeCosts.Length - 1 || upgradeCosts[elevatorLevel+1] > GameManager.Instance.Money) // maxed || no money
            return;
        GameManager.Instance.Money -= upgradeCosts[++elevatorLevel];
        if (elevatorLevel == 0) // buy
        {
            gameObject.SetActive(true);
            //init sprites
            int layer = towerLevel.GetComponent<SpriteRenderer>().sortingOrder - 300;
            sm.frontSortingOrder = layer;
            sm.backSortingOrder = layer - 1;
            bucket.sortingOrder = layer;
        }
        elevatorLimit = upgradesElevators[elevatorLevel];
        stashLimit = upgradeStashes[elevatorLevel];

        UpdateSprites();
    }

    private void UpdateSprites()
    {
        if (stashCurrent == 0)
            stash.sprite = stash0;
        else if (stashCurrent == stashLimit)
            stash.sprite = stash2;
        else
            stash.sprite = stash1;

        if (elevatorCurrent == elevatorLimit)
            bucket.sprite = bucketsFull[elevatorLevel];
        else
            bucket.sprite = bucketsEmpty[elevatorLevel];
    }

    public void TryPickUp(Worker worker)
    {
        if (stashCurrent > 0)
        {
            stashCurrent--;
            worker.hasRock = true;
            ts.StartGoingUp(worker);
            UpdateSprites();
        }
        else
            worker.NextOrder();
    }

    public void TryPlaceDown(Worker worker)
    {
        if(state == ElevatorState.NotFull)
        {
            elevatorCurrent++;
            if (elevatorCurrent == elevatorLimit)
                state = ElevatorState.FullWaiting;
            worker.hasRock = false;
            ts.StartGoingDown(worker);
            UpdateSprites();
        }
        else
            worker.NextOrder();
    }

    private void Update()
    {
        switch (state)
        {
            case ElevatorState.NotFull:
                //wait
                break;
            case ElevatorState.FullWaiting:
                if(stashLimit-stashCurrent >= elevatorCurrent)
                    state = ElevatorState.TravelUp;
                break;
            case ElevatorState.TravelUp:
                if (elevGO.transform.position.y >= yElevMax.transform.position.y)
                {
                    stashCurrent += elevatorCurrent;
                    elevatorCurrent = 0;
                    state = ElevatorState.TravelDown;
                    UpdateSprites();
                }
                else
                    elevGO.transform.Translate(Vector3.up * speed * Time.deltaTime * GameManager.Instance.GameSpeed);
                break;
            case ElevatorState.TravelDown:
                if (elevGO.transform.position.y <= yElevMin.transform.position.y)
                    state = ElevatorState.NotFull;
                else
                    elevGO.transform.Translate(Vector3.down * speed * Time.deltaTime * GameManager.Instance.GameSpeed);
                break;
            default:
                break;
        }
    }
}
