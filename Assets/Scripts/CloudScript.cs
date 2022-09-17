using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    public float speed, life;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed*Time.deltaTime,0);
        life -= Time.deltaTime;
        if (life < 0)
            Destroy(gameObject);
    }
}
