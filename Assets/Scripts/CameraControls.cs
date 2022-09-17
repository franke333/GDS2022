using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float limitDown, _limitUp;
    public float speed = 1f;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var delta = Input.mouseScrollDelta;
        if (delta.y == 0)
            return;
        float limitUp = Tower.Instance.levels.Count * Tower.Instance.LayerHeight + _limitUp;
        cam.transform.Translate(delta * speed);
        if(cam.transform.position.y < limitDown)
        {
            cam.transform.position = new Vector3(0, limitDown, cam.transform.position.z);
        }
        if (cam.transform.position.y > limitUp)
        {
            cam.transform.position = new Vector3(0, limitUp, cam.transform.position.z);
        }
    }
}
