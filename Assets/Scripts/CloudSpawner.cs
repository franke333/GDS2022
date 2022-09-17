using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public List<Sprite> clouds;

    public float heightMin, heightMax;

    public float waitMin, waitMax;

    public float life;

    public float speedMin, speedMax;

    private float waitTime = 0;

    public bool useSqrt = false;


    private void SpawnCloud()
    {
        GameObject cloud = new GameObject("Cloud");
        cloud.transform.SetParent(transform);

        var sr = cloud.AddComponent<SpriteRenderer>();
        sr.sprite = clouds[Random.Range(0, clouds.Count)];
        sr.sortingOrder = -800;

        var cs = cloud.AddComponent<CloudScript>();
        cs.speed = Random.Range(speedMin, speedMax);
        cs.life = life / cs.speed;


        float height = !useSqrt ? Random.Range(0f, 1f) * (heightMax - heightMin) + heightMin
            : Mathf.Sqrt(Random.Range(0f, 1f)) * (heightMax - heightMin) + heightMin;
        cloud.transform.position = new Vector3(transform.position.x, height);
    }


    private void Update()
    {
        waitTime -= Time.deltaTime;
        if (waitTime < 0)
        {
            waitTime = Random.Range(waitMin, waitMax);
            SpawnCloud();
        }
    }
}
