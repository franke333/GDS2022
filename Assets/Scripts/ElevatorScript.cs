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
    Sprite bucketEmpty, bucketFull, stash0, stash1, stash2;

    TransportStation ts;
    ElevatorState state;

    private void Start()
    {
        ts = TransportStation.Instance as TransportStation;
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
            bucket.sprite = bucketFull;
        else
            bucket.sprite = bucketEmpty;
    }

    public void TryPickUp(Worker worker)
    {
        if (stashCurrent > 0)
        {
            stashCurrent--;
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
                    elevGO.transform.Translate(Vector3.up * speed * Time.deltaTime);
                break;
            case ElevatorState.TravelDown:
                if (elevGO.transform.position.y <= yElevMin.transform.position.y)
                    state = ElevatorState.NotFull;
                else
                    elevGO.transform.Translate(Vector3.down * speed * Time.deltaTime);
                break;
            default:
                break;
        }
    }
}
