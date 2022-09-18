using UnityEngine;
using UnityEngine.Events;

public class SpriteButton : MonoBehaviour
{
    public UnityEvent onClickEvents;
    private void OnMouseDown()
    {
        // Runs by pressing the button
        onClickEvents.Invoke();
    }

}
